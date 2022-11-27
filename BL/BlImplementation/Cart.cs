using BlApi;
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
            //נקבל פה רשימה של כול המוצרים שלנו
            IEnumerable<DO.Product> DO_product = Dal.product.ReadAll();


            foreach (var item in cart.listOfOrder)
            {
                //אם זה כבר קיים אצלנו בעגלה
                if (item.ProductID == productID)
                {
                    //נחפס את אותו מוצר בחנות
                    foreach (var product in DO_product)
                    {
                        //ברגע שנגיע אליו נבדוק אם נשאר מוצרים
                        if (product.InStoke > 0 && product.ID == productID)
                        {
                            //נעדכן את הכמות מוצרים באחד והמחיר
                            item.Amount++;
                            item.TotalPrice = item.Amount * product.Price;

                            //עדכון מחיר הכולל של כול ההזמנה
                            cart.TotalPrice += item.TotalPrice;

                            return cart;
                        }
                    }
                    //לא יתכן שתהיה שגיאה שבכלל לא מצאנו את המוצר אבל יתכן שלא נשאר במלאי 
                    throw new NotImplementedException();
                }


            }
            //אם המוצר לא קיים אצלנו בעגלה
            //נחפס את אותו מוצר בחנות
            foreach (var product in DO_product)
            {
                //נחפס את המוצר לפי הזאות שלו
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
            //במקרה שלא מצאני את המוצר את נזרוק חריגה
            throw new NotImplementedException();
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
            //אם אני מקבל 0 אז אני לא משנה כלום לכן נחזיר מיד
            if (productQuantity == 0)
            {
                return cart;
            }

            //נקבל פה רשימה של כול המוצרים שלנו
            IEnumerable<DO.Product> DO_product = Dal.product.ReadAll();

            foreach (var item in cart.listOfOrder)
            {
                if (item.ProductID == productID)
                {

                    if (productQuantity > 0)//אם מוסיפים פריטים
                    {
                        //אז נבצע את פונקצית ההוספה ככמות הפעמים 
                        for (int i = 0; i < productQuantity; i++)
                        {
                            cart = this.AddProduct(cart, productID);

                        }
                        return cart;
                    }
                    else//אם אני מחסיר מוצרים
                    {
                        if (item.Amount - productQuantity > 0)//אם אני מסיר אבל עדין יש מוצרים
                        {
                            //נעדכן את הכמות והמחיר הכולל של המוצר
                            item.Amount = item.Amount - productQuantity;
                            item.TotalPrice = item.Amount * item.Price;

                            //נעדכן את המחיר הכולל של ההזמנה
                            cart.TotalPrice -= productQuantity * item.Price;

                            return cart;
                        }
                        else//אם נגמר כול המוצרים
                        {
                            //נעדכן את נמחיר
                            cart.TotalPrice -= item.TotalPrice;

                            //נמחוק אותו מההזמנה
                            cart.listOfOrder.Remove(item);

                            return cart;
                        }
                    }

                }
                //במקרה שלא מצאנו בכלל את המוצר
                throw new NotImplementedException();
            }
            return cart;//נחזיר בלי שינוי
        }
        catch (Exception exeption)
        {

            throw exeption;
        }
    }

    public void OrderConfirmation(BO.Cart cart)
    {
        try
        {
            //נבדוק שיש בכלל שם חוקי
            if (cart.CustomerName != null && cart.CustomerEmail != null && cart.CustomerAddress != null)
            {
                IEnumerable<DO.Product> DO_product = Dal.product.ReadAll();

                //בכול התהליך הזה אני בודק שרשימת ההזמנה שלי תקינה
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
                        //זה אומר שמוצר אחד שישי לי ברשימה אין לי אותו במוצרים
                        throw new NotImplementedException();
                    }
                }

                //בכול התהליך הזה אני בודק שרשימת ההזמנה שלי תקינה
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
                        //זה אומר שמוצר אחד שישי לי ברשימה אין לי אותו במוצרים
                        throw new NotImplementedException();
                    }
                }

                //עדכון המוצרים מהנתונים שלנו
                foreach (var item in cart.listOfOrder)
                {
                    foreach (var product in DO_product)
                    {
                        if (item.ProductID == product.ID)
                        {

                            if (product.InStoke - item.Amount == 0)//אם סימתי בדיוק את המוצרים
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
                                //אין מספיק מוצרים
                                throw new NotImplementedException();
                            }
                        }
                    }
                }


                //נוסיף הזמנה לנתונים שלנו 
                DO.Order newOrder = new DO.Order();

                newOrder.CustomerName = cart.CustomerName;
                newOrder.CustomerEmail = cart.CustomerEmail;
                newOrder.CstomerAddress = cart.CustomerAddress;
                newOrder.OrderDate = DateTime.Now;

                int OrderID = Dal.order.Create(newOrder);

                //נוסחף פריט הזמנה 
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
            //שם לא חוקי
            throw new NotImplementedException();
        }
        catch (Exception exeption)
        {

            throw exeption;
        }
    }
}
