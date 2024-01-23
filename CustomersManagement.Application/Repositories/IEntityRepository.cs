namespace CustomersManagement.Application.Repositories;


public interface IEntityRepository<T> where T : class, new()
{
    void Insert(T entity);
    IQueryable<T> GetAllQueryable();
}
