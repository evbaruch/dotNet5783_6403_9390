using DalApi;
using DO;
using System.Reflection.Emit;

namespace Dal;

public class DalUser : IUser
{
    public int Create(User user)
    {
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
            DataSource.listUser.Add(user);
            return 0;
        }
    }

    public User Read(User user)
    {
        User? isNULL = ReadObject(a => a?.UserName == user.UserName);
        int I = DataSource.listUser.FindIndex(x => x?.UserName == isNULL?.UserName);
        if (I != -1) // if the ID exist return the details else throw an Error
        {

            return (User)DataSource.listUser[I];

        }
        else
        {
            throw new IDWhoException("Delete range Error ");
        }
    }
    
    public void Update(User user)
    {
        User? isNULL = ReadObject(a => a?.UserName == user.UserName);
        int I = DataSource.listUser.IndexOf(isNULL);
        if (I != -1) // if the ID exist update the details else throw an Error
        {
            DataSource.listUser[I] = user;
        }
        else
        {
            throw new IDWhoException("object doesn't exist - Update");
        }
    }
    public void Delete(User user)
    {
        User? isNULL = ReadObject(a => a?.UserName == user.UserName);
        int I = DataSource.listUser.IndexOf(isNULL);
        if (I != -1) // if the ID exist delete the details else throw an Error
        {
            DataSource.listUser.Remove(user);
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error");
        }
    }

    public IEnumerable<User?> ReadAll(Func<User?, bool>? func = null)
    {
        //List<Product?> finalResult = new List<Product?>();
        if (func != null)
        {
            var finalResult = from item in DataSource.listUser
                              where func(item)
                              select item;

            return finalResult;
        }
        else
        {
            List<User?> finalResult = new List<User?>();

            finalResult = DataSource.listUser;
            return finalResult;
        }
    }

    public User ReadObject(Func<User?, bool>? func)
    {
        if(func != null)
        {
            var user = DataSource.listUser.Find(x => func(x));
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
