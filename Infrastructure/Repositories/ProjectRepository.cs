using Domain.Model;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProjectRepository(
    FocusFlowContext context)
    : Repository<Project>(context), IProjectRepository
{
    public async Task<bool> IsInUseAsync(int id)
    {
        return await context.Tasks.AnyAsync(x => x.ProjectId == id);
    }
}