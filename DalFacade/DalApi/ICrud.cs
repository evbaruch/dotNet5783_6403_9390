using DO;
namespace DalApi;

public interface ICrud<T> where T : struct
{
    //Create,Delete,Update,Read

    public int Create(T entity);
    public T Read(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public IEnumerable<T?> ReadAll(Func<T?, bool>? func = null);
    public IEnumerable<T?> Read(Func<T?, bool>? func);

}
