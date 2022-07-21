namespace Receipt_Micro_Service.Repisotory.Interfaces
{
    public interface IRepository<T>
    {
        T Save(T obj);
    }
}