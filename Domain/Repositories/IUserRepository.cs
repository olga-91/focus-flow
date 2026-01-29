using Domain.Model;

namespace Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> UserExists(string username);
    Task<User?> GetByCredentials(string username, string password);
}