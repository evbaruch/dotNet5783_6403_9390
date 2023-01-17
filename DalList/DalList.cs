using DalApi;
using DO;
using System.Diagnostics.Metrics;
using System.Security.Principal;

namespace Dal;
internal sealed class DalList : IDal
{
    //Here, instantiation is triggered by the first reference to the static member of
    //the nested class, which only occurs in Instance.This means the implementation is
    //fully lazy, but has all the performance benefits of the normal Singleton. Note that
    //although nested class have access to the enclosing class's private members,
    //the reverse is not true, hence the need for instance to be internal here. That
    //doesn't raise any other problems, though, as the class itself is private. The
    //code is a bit more complicated in order to make the instantiation lazy.

    class Nested
    {
        static Nested() { }
        internal static readonly DalList s_instance = new DalList();
    }

    static DalList() { }
    private DalList() { }
    public static DalList Instance { get => Nested.s_instance; }


    public IOrder order => new DalOrder();
    public IOrderItem orderItem => new DalOrderItem();
    public IProduct product => new DalProduct();
    public IUser user => new DalUser();

}
