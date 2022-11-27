﻿using Dal;
using DalApi;
using BlApi;
using IProduct = BlApi.IProduct;
using System.Linq;

namespace BlImplementation;

internal class Product : IProduct
{
    private IDal Dal => new DalList();

    public IEnumerable<BO.ProductForList> Products()
    {
        IEnumerable<DO.Product> products = Dal.product.ReadAll().ToList();
        IEnumerable<BO.ProductForList> ProductForList = new List<BO.ProductForList>();
        foreach (var item in products) // go over the products and get all the data from DO 
        {
            BO.ProductForList temp = new(){ID = item.ID,Name = item.Name,Price = item.Price,Category = (BO.Enums.productsCategory?)item.Category };
            ProductForList.Append(temp);
        }
        return ProductForList;
    }

    public BO.Product ProductDetails(int id)
    {
        try
        {


            BO.Product product = new() { };
            if (id > 0)
            {
                DO.Product temp = new() { ID = id };
                temp = Dal.product.Read(temp);
                product = new() { ID = temp.ID, Name = temp.Name, Price = temp.Price, Category = (BO.Enums.productsCategory?)temp.Category, InStock = temp.InStoke };
                return product;
            }
            else
            {
                throw new Exception("product don't exist - BlIm");
            }
        }
        catch (Exception exeption)
        {

            throw exeption;
        }
    }

    public BO.ProductItem ProductDetails(int id, BO.Cart cart)
    {
        try
        {
            BO.ProductItem productItem = new() { };
            if (id > 0)
            {
                productItem.ID = id;
                productItem.Name = cart.CustomerName;
                foreach (var item in cart.listOfOrder) // looking for an productItem with the same id product in order to find out the price  
                {
                    if (item.ProductID == productItem.ID)
                    {
                        productItem.Price = item.Price;
                        break;
                    }
                }
                foreach (var item in cart.listOfOrder) // looking for an productItem with the same id product in order to find out the amount
                {
                    if (item.ProductID == productItem.ID)
                    {
                        productItem.Amount++;
                    }
                }
                productItem.InStock = (productItem.Amount>0) ? true : false;
                DO.Product preProduct = new() { ID = productItem.ID, Name = productItem.Name };
                preProduct = Dal.product.Read(preProduct);
                productItem.Category = (BO.Enums.productsCategory?)preProduct.Category;
                return productItem;
            }
            else
            {
                throw new Exception("product item don't exist - BlIm");
            }
        }
        catch (Exception exeption)
        {
            throw exeption;
        }
    }

    public void DeleteProduct(int id)
    {
        try
        {
            bool exist = false;
            if (id > 0)
            {
                foreach (var item in Dal.product.ReadAll()) // check if the product exist already
                {
                    if (id == item.ID)
                    {
                        exist = true;
                    }
                }
                if (!exist)
                {
                    DO.Product delete = new() { ID = id };
                    Dal.product.Delete(delete);
                }
                else
                {
                    throw new Exception("can't delete ,exist in an order");
                }
            }
            else
            {
                throw new Exception("out of range exeption");
            }
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
            if ((product == null) && (product.ID > 0) && (product.Name != "") && (product.Price > 0) && (product.InStock > 0))
            {
                DO.Product addproduct = new() { Price = product.Price, Name = product.Name, Category = (DO.Enums.productsCategory?)product.Category, ID = product.ID, InStoke = product.InStock };
                Dal.product.Update(addproduct);
            }
            else
            {
                throw new Exception("the product was not able to be updated");
            }
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
            if ((product == null) && (product.ID > 0) && (product.Name != "") && (product.Price > 0) && (product.InStock > 0))
            {
                DO.Product addproduct = new() { Price = product.Price, Name = product.Name, Category = (DO.Enums.productsCategory?)product.Category, ID = product.ID, InStoke = product.InStock };
                Dal.product.Create(addproduct);
            }
            else
            {
                throw new Exception("the product was not able to be added");
            }
        }
        catch (Exception exeption)
        {
            throw exeption;
        }
    }
}
