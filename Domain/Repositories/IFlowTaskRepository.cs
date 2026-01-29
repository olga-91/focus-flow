using Domain.Model;
using Domain.Queries;

namespace Domain.Repositories;

public interface IFlowTaskRepository : IRepository<FlowTask>
{
    Task<List<FlowTask>> FilterAsync(string? projectName, int? statusId, int? priorityId);
    Task<Dictionary<string, ProjectStatisticsQuery>> GetProjectLevelStatisticsAsync();
}