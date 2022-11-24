using BO;
namespace BlApi;
/// <summary>
/// 
/// </summary>
internal interface ICart
{
    /// <summary>
    /// Adding a product to the cart (for catalog screen, product details screen)
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <returns></returns>
    public Cart AddProduct(Cart cart, int productID);
    /// <summary>
    /// Updating the quantity of a product in the shopping cart (for the shopping cart screen)
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <param name="productQuantity"></param>
    /// <returns></returns>
    public Cart UpdateProductQuantity(Cart cart, int productID, int productQuantity);
    /// <summary>
    /// Basket confirmation for order \ placing an order (for shopping basket screen or order completion screen)
    /// </summary>
    /// <param name="cart"></param>
    public void OrderConfirmation(Cart cart);
}
