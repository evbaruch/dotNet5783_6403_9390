using DalApi;
using BlApi;
using IProduct = BlApi.IProduct;
using System.Linq;
using BO;
using System.Reflection.Metadata;
using DO;

namespace BlImplementation;

internal class Product : IProduct
{
    DalApi.IDal? dal = DalApi.Factory.Get();


    public IEnumerable<BO.ProductForList> Products(Func<DO.Product?, bool>? filter = null)
    {
        var products = dal.product.ReadAll(filter).ToList();
        var productForLists = products.Select(product => new BO.ProductForList
        {
            ID = (int)(product?.ID),
            Name = product?.Name,
            Price = product?.Price,
            Category = (BO.Enums.productsCategory?)product?.Category

        }).ToList();

        return productForLists;
    }

    public IEnumerable<BO.ProductItem> ProductItemList(Func<DO.Product?, bool>? filter = null)
    {
        var products = dal.product.ReadAll(filter).ToList();
        var productItem = products.Select(item => new BO.ProductItem
        {
            ID = (int)(item?.ID),
            Name = item?.Name,
            Price= item?.Price,
            Category = (BO.Enums.productsCategory?)item?.Category,
            Amount = 0,
            InStock = item?.InStock > 0
        }).ToList();

        return productItem;
    }


    public BO.Product ProductDetails(int id)
    {
        try
        {
            BO.Product product = new() { };
            if (id > 0)
            {
                DO.Product temp = new() { ID = id };
                temp = dal.product.Read(temp);
                product = new() { ID = temp.ID, Name = temp.Name, Price = temp.Price, Category = (BO.Enums.productsCategory?)temp.Category, InStock = temp.InStock };
                return product;
            }
            else
            {
                throw new DataNotFoundException(" ", new Exception("BlImplementation->Product->ProductDetails = product don't exist - BlIm"));
            }
        }
        catch (Exception exeption)
        {

            throw exeption;
        }
    }

    public BO.ProductItem ProductDetails(int productID, BO.Cart cart)
    {
        if (productID <= 0)//נבדוק שקיבלנו תז תקין
        {
            throw new DataNotFoundException("Product does not exist", new Exception("BlImplementation->Product->GetProductDetails = product don't exist - BlIm"));
        }

        var orderItems = cart.listOfOrderItem;
        if (orderItems.Count == 0)//נבדוק אם בכלל קיים מוצרים בעגלה
        {
            throw new DataNotFoundException("Product does not exist", new Exception("BlImplementation->Product->GetProductDetails = product don't exist - BlIm"));
        }

        var orderItem = orderItems.FirstOrDefault(item => item.ProductID == productID);
        if (orderItem == null)//נבדוק אם המוצר שלנו קיים בעגלה
        {
            throw new DataNotFoundException("Product does not exist", new Exception("BlImplementation->Product->GetProductDetails = product don't exist - BlIm"));
        }

        var product = dal.product.ReadObject(item => item?.ID == productID);
        if (product.ID == -1)//נבדוק אם המוצר הזה קיים בכלל במוצרים
        {
            throw new DataNotFoundException("Product does not exist", new Exception("BlImplementation->Product->GetProductDetails = product don't exist - BlIm"));
        }

        return new BO.ProductItem
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (BO.Enums.productsCategory)product.Category,
            Amount = orderItem.Amount,
            InStock = product.InStock > 0
        };
    }

    public void DeleteProduct(int id)
    {
        try
        {
            bool exist = false;
            if (id > 0)
            {

                if (dal.product.ReadAll(a => id == a?.ID).Count() > 0)
                {
                    exist = true;
                }

                if (!exist) // not exist
                {
                    DO.Product delete = new() { ID = id };
                    dal.product.Delete(delete);
                }
                else
                {
                    throw new DataNotFoundException(" ", new Exception("BlImplementation->Product->DeleteProduct = can't delete ,exist in an order"));
                }
            }
            else
            {
                throw new IncorrectDataException(" ", new Exception("BlImplementation->Product->DeleteProduct = out of range exeption"));
            }
        }
        catch (DO.IDWhoException)
        {
            throw new DataNotFoundException(" ", new Exception("IDWhoException was throw"));
        }
        catch (DO.ISawYouAlreadyException)
        {
            throw new IncorrectDataException(" ", new Exception("ISawYouAlreadyException was throw"));
        }
        catch (Exception exeption)
        {
            throw exeption;
        }
    }

    public void UpdateProduct(BO.Product product)
    {
        try
        {
            if ((product != null) && (product.ID > 0) && (product.Name != "") && (product.Price > 0) && (product.InStock > 0)) // only if all of the details are legal
            {
                DO.Product addproduct = new() { Price = product.Price, Name = product.Name, Category = (DO.Enums.productsCategory?)product.Category, ID = product.ID, InStock = product.InStock };
                dal.product.Update(addproduct);
            }
            else
            {
                throw new DataNotFoundException(" ", new Exception("BlImplementation->Product->UpdateProduct = the product was not able to be updated"));
            }
        }
        catch (DO.IDWhoException)
        {
            throw new DataNotFoundException(" ", new Exception("IDWhoException was throw"));
        }
        catch (DO.ISawYouAlreadyException)
        {
            throw new IncorrectDataException(" ", new Exception("ISawYouAlreadyException was throw"));
        }
        catch (Exception exeption)
        {
            throw exeption;
        }
    }

    public void AddProduct(BO.Product product)
    {
        try
        {
            if ((product != null) && (product.ID >= 0) && (product.Name != "") && (product.Price > 0) && (product.InStock > 0)) // only if all of the details are legal
            {
                DO.Product addproduct = new() { Price = product.Price, Name = product.Name, Category = (DO.Enums.productsCategory?)product.Category, ID = product.ID, InStock = product.InStock };
                dal.product.Create(addproduct);
            }
            else
            {
                throw new IncorrectDataException(" ", new Exception("BlImplementation->Product->AddProduct = the product was not able to be updated"));
            }
        }
        catch (DO.IDWhoException)
        {
            throw new DataNotFoundException(" ", new Exception("IDWhoException was throw"));
        }
        catch (DO.ISawYouAlreadyException)
        {
            throw new IncorrectDataException(" ", new Exception("ISawYouAlreadyException was throw"));
        }
        catch (Exception exeption)
        {
            throw exeption;
        }
    }
}
