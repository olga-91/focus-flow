namespace Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    Task<T> FindAsync(params object[] keyValues);
    T Update(T entity);
    Task<int> SaveChangesAsync();
    Task<List<T>> GetAllAsync();
}