using CustomersManagement.Application.Repositories;
using CustomersManagement.Infrastructure.DBContext;

namespace CustomersManagement.Infrastructure.RepositoriesImplemetation;

public class EntityRepository<T> : IEntityRepository<T> where T : class, new()
{
    private DataContext _dbContext;

    public EntityRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> GetAllQueryable()
    {
        return _dbContext.Set<T>();
    }

    public void Insert(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        _dbContext.SaveChanges();
    }
}
