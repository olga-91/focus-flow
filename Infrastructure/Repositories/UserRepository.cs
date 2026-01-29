using Domain.Model;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(
    FocusFlowContext context)
    : Repository<User>(context), IUserRepository
{
    public async Task<bool> UserExists(string username)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username);

        return user != null;
    }

    public async Task<User?> GetByCredentials(string username, string password)
    {
        return await context.Users
            .SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
    }
}