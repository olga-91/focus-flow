using Domain.Model;
using Domain.Queries;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FlowTaskRepository(FocusFlowContext context)
    : Repository<FlowTask>(context), IFlowTaskRepository
{
    public async Task<List<FlowTask>> FilterAsync(string? projectName,
        int? statusId, int? priorityId)
    {
        var query = context.Tasks
            .Include(x => x.Project)
            .Include(x => x.Status)
            .Include(x => x.Priority)
            .Include(x => x.AssignedUser)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(projectName))
        {
            query = query.Where(x => x.Project.Name.Contains(projectName));
        }

        if (statusId != null)
        {
            query = query.Where(x => x.StatusId == statusId);
        }

        if (priorityId != null)
        {
            query = query.Where(x => x.PriorityId == priorityId);
        }

        return await query.ToListAsync();
    }

    public async Task<Dictionary<string, ProjectStatisticsQuery>> GetProjectLevelStatisticsAsync()
    {
        var completedStatus = await context.Statuses.SingleAsync(x => x.Name == "Done");

        return await context.Tasks
            .Where(x => x.ProjectId != null)
            .GroupBy(x => x.Project.Name)
            .ToDictionaryAsync(x => x.Key, x => new ProjectStatisticsQuery
            {
                CompletedTasks = x.Count(y => y.StatusId == completedStatus.Id),
                OverdueTasks = x.Count(y => y.DueDate < DateTime.Now),
                TotalTasks = x.Count()
            });
    }
}