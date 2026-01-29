using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class Repository<T>(FocusFlowContext context) : IRepository<T> where T : class
{
    public async Task<T> AddAsync(T entity)
    {
        var change = await context.Set<T>().AddAsync(entity);
        return change.Entity;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T> FindAsync(params object[] keyValues)
    {
        return await context.Set<T>().FindAsync(keyValues);
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        context.Set<T>().RemoveRange(entities);
    }

    public T Update(T entity)
    {
        context.Attach(entity);
        context.Set<T>().Update(entity);

        return entity;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}