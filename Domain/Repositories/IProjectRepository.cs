using Domain.Model;

namespace Domain.Repositories;

public interface IProjectRepository : IRepository<Project>
{
    Task<bool> IsInUseAsync(int id);
}