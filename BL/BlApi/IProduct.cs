using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
/// <summary>
/// Operation on the Product
/// </summary>
public interface IProduct
{
    /// <summary>
    /// asking the products list(admin and catalog)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product> Products();
    /// <summary>
    /// request for a product (user and admin)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Product ProductDetails(int id);
    /// <summary>
    ///  request for a product (catalog)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public ProductItem ProductDetails(int id,Cart cart);
    /// <summary>
    /// add a product (admin)
    /// </summary>
    /// <param name="product"></param>
    public void AddProduct(Product product);
    /// <summary>
    /// delete a product (admin)
    /// </summary>
    /// <param name="id"></param>
    public void DeleteProduct(int id);
    /// <summary>
    /// update a product (admin)
    /// </summary>
    /// <param name="product"></param>
    public void UpdateProduct(Product product);
}
