﻿using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Cart : ICart
{
    private IDal Dal = new DalList();

    public BO.Cart AddProduct(BO.Cart cart, int productID)
    {
        try
        {
            //We will get a list of all our products here
            IEnumerable<DO.Product> DO_product = Dal.product.ReadAll();


            foreach (var item in cart.listOfOrder)
            {
                //If it already exists in our cart
                if (item.ProductID == productID)
                {
                    //Grab the same product in the store
                    foreach (var product in DO_product)
                    {
                        //As soon as we reach it we will check if there are any products left
                        if (product.InStoke > 0 && product.ID == productID)
                        {
                            //We will update the quantity of products in one and the price
                            item.Amount++;
                            item.TotalPrice = item.Amount * product.Price;

                            //Update the total price of the entire order
                            cart.TotalPrice += item.TotalPrice;

                            return cart;
                        }
                    }
                    //It is not possible that there will be an error that we did not find the product at all, but it is possible that it is not left in stock 
                    throw new DataNotFoundException("BlImplementation->ICart->AddProduct = Product not in stock");
                }


            }
            //If the product is not in our cart
            //Grab the same product in the store
            foreach (var product in DO_product)
            {
                //We seize the product according to its appearance
                if (product.ID == productID)
                {
                    BO.OrderItem orderItem = new BO.OrderItem();

                    orderItem.Price = product.Price;
                    orderItem.Amount = 1;
                    orderItem.TotalPrice = product.Price;

                    cart.TotalPrice += product.Price;
                    cart.listOfOrder.Add(orderItem);

                    return cart;
                }
            }
            //In case I can't find the product, we will throw an exception
            throw new DataNotFoundException("BlImplementation->ICart->AddProduct = Product not found");
        }
        catch (Exception exeption)
        {
             
            throw exeption;
        }
    }

    public BO.Cart UpdateProductQuantity(BO.Cart cart, int productID, int productQuantity)
    {
        try
        {
            //If I get 0 then I don't change anything so we return immediately
            if (productQuantity == 0)
            {
                return cart;
            }

            //We will get a list of all our products here
            IEnumerable<DO.Product> DO_product = Dal.product.ReadAll();

            foreach (var item in cart.listOfOrder)
            {
                if (item.ProductID == productID)
                {

                    if (productQuantity > 0)//If you add items
                    {
                        //So we will perform the addition function the number of times 
                        for (int i = 0; i < productQuantity; i++)
                        {
                            cart = this.AddProduct(cart, productID);

                        }
                        return cart;
                    }
                    else//If I'm missing products
                    {
                        if (item.Amount - productQuantity > 0)//If I remove but there are still products
                        {
                            //We will update the total quantity and price of the product
                            item.Amount = item.Amount - productQuantity;
                            item.TotalPrice = item.Amount * item.Price;

                            //We will update the total price of the order
                            cart.TotalPrice -= productQuantity * item.Price;

                            return cart;
                        }
                        else//If all products run out
                        {
                            //We will update you
                            cart.TotalPrice -= item.TotalPrice;

                            //We will delete it from the order
                            cart.listOfOrder.Remove(item);

                            return cart;
                        }
                    }

                }
                //In case we did not find the product at all
                throw new DataNotFoundException("BlImplementation->ICart->UpdateProductQuantity = Product not found");
            }
            return cart;//We will return without change
        }
        catch (Exception exeption)
        {

            throw exeption;
        }
    }

    public void OrderConfirmation(BO.Cart cart) // the Customer pesonal details are already in the system ,no need to send them
    {
        try
        {
            //We will check that there is even a valid name
            if (cart.CustomerName != null && cart.CustomerEmail != null && cart.CustomerAddress != null)
            {
                IEnumerable<DO.Product> DO_product = Dal.product.ReadAll();

                //Throughout this process I check that my order list is correct
                foreach (var item in cart.listOfOrder)
                {
                    bool flag = false;
                    foreach (var product in DO_product)
                    {
                        if (item.ProductID == product.ID)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        //This means that one product that I have in the list I do not have it in the products
                        throw new IncorrectDataException("BlImplementation->ICart->OrderConfirmation = There is a non-existent product in the cart");
                    }
                }

                //Updating the products from our data
                foreach (var item in cart.listOfOrder)
                {
                    foreach (var product in DO_product)
                    {
                        if (item.ProductID == product.ID)
                        {

                            if (product.InStoke - item.Amount == 0)//If I marked the products exactly
                            {
                                Dal.product.Delete(product);
                            }
                            if (product.InStoke - item.Amount > 0)
                            {
                                DO.Product newProduct = new DO.Product();

                                newProduct.ID = item.ProductID;
                                newProduct.Name = item.Name;
                                newProduct.Price = item.Price;
                                newProduct.Category = product.Category;
                                newProduct.Category = newProduct.Category - item.Amount;

                                Dal.product.Update(newProduct);
                            }
                            else
                            {
                                //Not enough products
                                throw new IncorrectDataException("BlImplementation->ICart->OrderConfirmation = Not enough products");
                            }
                        }
                    }
                }


                //We will add an order to our data
                DO.Order newOrder = new DO.Order();

                newOrder.CustomerName = cart.CustomerName;
                newOrder.CustomerEmail = cart.CustomerEmail;
                newOrder.CstomerAddress = cart.CustomerAddress;
                newOrder.OrderDate = DateTime.Now;

                int OrderID = Dal.order.Create(newOrder);

                //Order item formula
                foreach (var item in cart.listOfOrder)
                {
                    DO.OrderItem newOrderItem = new DO.OrderItem();

                    newOrderItem.ProductID = item.ProductID;
                    newOrderItem.OrderID = OrderID;
                    newOrderItem.Price = item.Price;
                    newOrderItem.Amount = item.Amount;

                    Dal.orderItem.Create(newOrderItem);
                }
            }
            //Invalid name
            throw new IncorrectDataException("BlImplementation->ICart->OrderConfirmation = Invalid name");
        }
        catch (Exception exeption)
        {

            throw exeption;
        }
    }
}
