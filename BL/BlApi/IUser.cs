using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

/// <summary>
/// Operation on the User
/// </summary>
public interface IUser
{
    /// <summary>
    /// asking the users list(admin)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<UserForList> Users(Func<DO.User?, bool>? func = null);
    /// <summary>
    /// request for a user (admin catalog)
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public User UserDetails(string userName);
    /// <summary>
    /// add a User (admin)
    /// </summary>
    /// <param name="user"></param>
    public void SighIn(User user);
    /// <summary>
    /// delete a user (user and admin)
    /// </summary>
    /// <param name="user"></param>
    public void SighOut(User user);
    /// <summary>
    /// update a User(admin)
    /// </summary>
    /// <param name="user"></param>
    public void UpdateUser(User user);
    /// <summary>
    /// check the uniqueness of the UserName
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public bool IsUserNameUnique(string userName);

    public IEnumerable<BO.ProductItem> UserProductItems(BO.Cart cart);


}
