using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IUser = BlApi.IUser;

namespace BlImplementation;

internal class User : IUser
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    public IEnumerable<UserForList> Users(Func<DO.User?, bool>? filter = null)
    {
        var users = dal.user.ReadAll(filter).ToList();
        var userForLists = users.Select(user => new BO.UserForList
        {
            UserName = user?.UserName,
            Address = user?.Address,
            Email = user?.Email,
            IsAdmin = user?.IsAdmin,
            NumOfOrder = user?.listOfOrder.Count()

        }).ToList();

        return userForLists;
    }

    public BO.User UserDetails(string userName)
    {
        try
        {
            Order order = new Order();
            BO.User user = new() { };
            if (userName != "" )
            {
                DO.User temp = new() { UserName = userName };
                temp = dal.user.Read(temp);
                user = new() { UserName = temp.UserName, Address = temp.Address, Email = temp.Email, IsAdmin = temp.IsAdmin, Password = temp.Password};
                user.listOfOrder = (from numbers in dal.user.Read(new() { UserName = userName}).listOfOrder
                                   select order.OrderForList(numbers)).ToList();
                user.currentCart = new BO.Cart();
                user.currentCart.listOfOrderItem = new List<BO.OrderItem>();
                user.currentCart.listOfOrderItem = (from move in dal.user.Read(new() {UserName = user.UserName }).CurrentOrder
                                                   select new BO.OrderItem { Amount = move.Amount , ID = move.OrderItemID , Name = userName, Price = move.Price , ProductID = move.ProductID , TotalPrice = move.Amount * move.Price}).ToList();
                return user;
            }
            else
            {
                throw new IncorrectDataException(" ", new Exception("BlImplementation->User->UserDetails = worng input - BlIm"));
            }
        }
        catch (Exception exeption)
        {

            throw new DataNotFoundException(" ", new Exception("BlImplementation->User->UserDetails = user don't exist - BlIm"));
        }
    }

    public void SighIn(BO.User user)
    {
        try
        {
            Order order = new Order();
            if ((user != null) && (user.UserName != "") && (user.Address != "") && (user.Password != null) && (user.Email != "")) // only if all of the details are legal
            {
                DO.User addUser = new() {UserName = user.UserName , Address = user.Address , Password = user.Password.ToString(), Email = user.Email , IsAdmin = user.IsAdmin };
                addUser.listOfOrder = new List<int>();
                dal.user.Create(addUser);
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

    public void SighOut(BO.User user)
    {
        try
        {
            bool exist = false;
            if (user.UserName != "" || user.UserName != null)
            {

                if (dal.user.ReadAll(a => user.UserName == a?.UserName).Count() > 0)
                {
                    exist = true;
                }

                if (!exist) // not exist
                {
                    DO.User delete = new() { UserName = user.UserName };
                    dal.user.Delete(delete);
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

    public void UpdateUser(BO.User user)
    {
        try
        {
            Order order = new Order();
            if ((user != null) && (user.UserName != "") && (user.Address != "") && (user.Password != null) && (user.Email != "")) // only if all of the details are legal
            {
                DO.User updUser = new() { UserName = user.UserName , Address = user.Address , Password = user.Password.ToString(), Email = user.Email , IsAdmin = false };
                updUser.listOfOrder =  (from element in user.listOfOrder
                                       select element.ID).ToList();
                updUser.CurrentOrder = (from move in user.currentCart.listOfOrderItem
                                       select new DO.OrderItem { Amount = move.Amount ,ProductID = move.ProductID , OrderItemID = move.ID , Price = move.Price}).ToList(); 
                dal.user.Update(updUser);
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

    public bool IsUserNameUnique(string userName)
    {
        var users = dal.user.ReadAll(x=>x?.UserName == userName);
        return users.Count() == 0;
    }

    public IEnumerable<BO.ProductItem> UserProductItems(BO.Cart? cart)
    {
        Product product = new Product();
        IEnumerable<BO.ProductItem> productItem = product.ProductItemList();

        if (cart == null || cart.listOfOrderItem == null)
        {
            return productItem;
        }


        foreach (var produc in productItem)
        {
            foreach (var order in cart.listOfOrderItem)
            {
                if (produc.ID == order.ProductID)
                {
                    produc.Amount = order.Amount;
                }
            }
        }


        return productItem;
    }
}
