using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalUser : IUser
{
    const string UserPath = "User";
    static XElement config = XmlTools.LoadConfig();

    public int Create(User user)
    {
        List<DO.User?> listUser = XmlTools.LoadListFromXMLSerializer<DO.User>(UserPath);
        try
        {
            if (user.UserName != null)
            {
                Read(new User() { UserName = user.UserName });
            }
            return -1;
        }
        catch (IDWhoException)
        {
            listUser.Add(user);
            XmlTools.SaveListToXMLSerializer(listUser, UserPath);
            return 0;
        }
    }

    public User Read(User user)
    {
        List<DO.User?> listUser = XmlTools.LoadListFromXMLSerializer<DO.User>(UserPath);
        User? isNULL = ReadObject(a => a?.UserName == user.UserName);
        int I = listUser.FindIndex(x=>x?.UserName == isNULL?.UserName);
        if (I != -1) // if the ID exist return the details else throw an Error
        {

            return (User)listUser[I];

        }
        else
        {
            throw new IDWhoException("Delete range Error ");
        }
    }

    public void Update(User user)
    {
        List<DO.User?> listUser = XmlTools.LoadListFromXMLSerializer<DO.User>(UserPath);
        User? isNULL = ReadObject(a => a?.UserName == user.UserName);
        int I = listUser.IndexOf(isNULL);
        if (I != -1) // if the ID exist update the details else throw an Error
        {
            listUser[I] = user;
            XmlTools.SaveListToXMLSerializer(listUser, UserPath);
        }
        else
        {
            throw new IDWhoException("object doesn't exist - Update");
        }
    }
    public void Delete(User user)
    {
        List<DO.User?> listUser = XmlTools.LoadListFromXMLSerializer<DO.User>(UserPath);
        User? isNULL = ReadObject(a => a?.UserName == user.UserName);
        int I = listUser.IndexOf(isNULL);
        if (I != -1) // if the ID exist delete the details else throw an Error
        {
            listUser.Remove(user);
            XmlTools.SaveListToXMLSerializer(listUser, UserPath);
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error");
        }
    }

    public IEnumerable<User?> ReadAll(Func<User?, bool>? func = null)
    {
        List<DO.User?> listUser = XmlTools.LoadListFromXMLSerializer<DO.User>(UserPath);
        //List<Product?> finalResult = new List<Product?>();
        if (func != null)
        {
            var finalResult = from item in listUser
                              where func(item)
                              select item;

            return finalResult;
        }
        else
        {
            List<User?> finalResult = new List<User?>();

            finalResult = listUser;
            return finalResult;
        }
    }

    public User ReadObject(Func<User?, bool>? func)
    {
        List<DO.User?> listUser = XmlTools.LoadListFromXMLSerializer<DO.User>(UserPath);
        if (func != null)
        {
            var user = listUser.Find(x => func(x));
            if (user != null)
            {
                return (User)user;
            }
        }

        return new()
        {
            UserName = null,
            Address = null,
            Email = null,
            Password = null,
            IsAdmin = null,
            listOfOrder = null,
        };
    }
}
