using BlApi;
using BO;

namespace BlImplementation;

internal class Cart : ICart
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    public BO.Cart AddProduct(BO.Cart cart, int productID)
    {
        // Get the product with the specified ID
        DO.Product? DO_product = dal.product.ReadObject(p => p?.InStoke > 0 && p?.ID == productID);

        // If the product was not found, throw an exception
        if (DO_product?.ID == -1)
        {
            throw new DataNotFoundException("Product not found", new Exception("BlImplementation->ICart->AddProduct"));
        }

        // Find the OrderItem that has the matching ProductID
        BO.OrderItem item = cart.listOfOrderItem.FirstOrDefault(i => i.ProductID == productID);

        // If an OrderItem was found with a matching ProductID
        if (item != null)
        {
            // Reset the previous price
            cart.TotalPrice -= item.TotalPrice;

            // Update the quantity and total price of the OrderItem
            item.Amount++;
            item.TotalPrice = item.Amount * DO_product?.Price;

            // Update the total price of the entire order
            cart.TotalPrice += item.TotalPrice;
        }
        // If an OrderItem with a matching ProductID was not found
        else
        {
            // Create a new OrderItem with the specified ProductID and price
            BO.OrderItem orderItem = new BO.OrderItem
            {
                ID = dal.orderItem.ReadAll().Max(i => i.Value.ID) + 1,
                Name = DO_product?.Name,
                Price = DO_product?.Price,
                Amount = 1,
                TotalPrice = DO_product?.Price,
                ProductID = productID
            };

            // Add the OrderItem to the list and update the total price of the cart
            cart.listOfOrderItem.Add(orderItem);
            cart.TotalPrice += DO_product?.Price;
        }

        return cart;
    }

    public BO.Cart UpdateProductQuantity(BO.Cart cart, int productID, int productQuantity)
    {
        if (productQuantity < 0)
        {
            throw new IncorrectDataException("A negative update has been entered", new Exception("BlImplementation->ICart->UpdateProductQuantity"));
        }

        var DO_product = dal.product.ReadAll();
        var matchingItem = cart.listOfOrderItem.FirstOrDefault(item => item.ProductID == productID);

        if (matchingItem == null)
        {
            throw new DataNotFoundException("Product not found", new Exception("BlImplementation->ICart->UpdateProductQuantity"));
        }

        if (productQuantity == 0)
        {
            //If a zero was entered then we will delete it from the cart
            cart.listOfOrderItem.Remove(matchingItem);
        }
        else
        {
            // Reset the previous price
            cart.TotalPrice -= matchingItem.TotalPrice;

            // Update the quantity and total price of the OrderItem
            matchingItem.Amount = productQuantity;
            matchingItem.TotalPrice = productQuantity * matchingItem.Price;

            // Update the total price of the entire order
            cart.TotalPrice += matchingItem.TotalPrice;
        }

        return cart;
    }

    public void OrderConfirmation(BO.Cart cart)
    {
        // We will check that there is a valid name, email, and address for the customer.
        if (string.IsNullOrEmpty(cart.CustomerName) || string.IsNullOrEmpty(cart.CustomerEmail) || string.IsNullOrEmpty(cart.CustomerAddress))
        {
            throw new IncorrectDataException("BlImplementation->ICart->OrderConfirmation = Invalid customer information provided");
        }

        IEnumerable<DO.Product?> products = dal.product.ReadAll();

        // Check that all products in the cart exist in the product list And  if products are missing.
        foreach (var orderItem in cart.listOfOrderItem)//I didn't change it to LINQ because it didn't make sense
        {
            DO.Product? product = products.FirstOrDefault(item => item?.ID == orderItem.ProductID);
            if (product == null)
            {
                throw new IncorrectDataException("BlImplementation->ICart->OrderConfirmation = There is a non-existent product in the cart");
            }
            if (product?.InStoke < orderItem.Amount)
            {
                throw new DataNotFoundException("BlImplementation->ICart->OrderConfirmation = Missing products for the product");
            }
        }


        //We will create a new order
        DO.Order newOrder = new DO.Order()
        {
            ID = dal.order.ReadAll().Max(i => i.Value.ID) + 1,
            CustomerName = cart.CustomerName,
            CustomerEmail = cart.CustomerEmail,
            CstomerAddress = cart.CustomerAddress,
            OrderDate = DateTime.Now
        };
        int orderID = dal.order.Create(newOrder);

        // Update the product quantities in the product list and update OrderItem.
        foreach (var orderItem in cart.listOfOrderItem)//I didn't change it to LINQ because it didn't make sense
        {
            DO.Product? product = products.FirstOrDefault(item => item?.ID == orderItem.ProductID);

            DO.Product newProduct = new DO.Product()
            {
                ID = (int)(product?.ID),
                Name = product?.Name,
                Price = product?.Price,
                Category = product?.Category,
                InStoke = product?.InStoke - orderItem.Amount
            };
            dal.product.Update(newProduct);

            DO.OrderItem newOrderItem = new DO.OrderItem()
            {
                ID = (int)orderItem?.ID,
                ProductID = (int)orderItem?.ProductID,
                OrderID = orderID,
                Price = orderItem.Price,
                Amount = orderItem.Amount,
            };
            dal.orderItem.Create(newOrderItem);
        }
    }
}