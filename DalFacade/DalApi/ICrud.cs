using DO;
namespace DalApi;

public interface ICrud<T>
{
    //Create,Delete,Update,Read
    public int Create(Order entity)
    {
        return -1;
    }
    public Order Read(Order entity)
    {
        return entity;
    }
    public void Update(Order entity)
    {

    }
    public void Delete(Order entity)
    {

    }

}
