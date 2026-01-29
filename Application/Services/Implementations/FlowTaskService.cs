using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Model;
using Domain.Repositories;

namespace Application.Services.Implementations;

public class FlowTaskService(
    IFlowTaskRepository repository) : IFlowTaskService
{
    public async Task<IEnumerable<FlowTaskDto>> FilterAsync(FilterDto filterDto)
    {
        var tasks = await repository.FilterAsync(filterDto.Project, filterDto.StatusId, filterDto.PriorityId);

        return tasks.Select(x => new FlowTaskDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            StatusId =  x.StatusId,
            StatusName =  x.Status?.Name,
            PriorityId = x.PriorityId,
            PriorityName =   x.Priority?.Name,
            ProjectName = x.Project?.Name,
            DueDate = x.DueDate,
            AssignedUser = x.AssignedUser?.Name
        });
    }

    public async Task<FlowTaskDto?> GetByIdAsync(int id)
    {
        var task = await repository.FindAsync(id);

        if (task == null)
        {
            throw new KeyNotFoundException($"FlowTask with id {id} not found");
        }

        return new FlowTaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate =  task.DueDate,
            AssignedUserId =  task.AssignedUserId,
            StatusId =  task.StatusId,
            PriorityId = task.PriorityId,
            ProjectId = task.ProjectId
        };
    }

    public async Task<IEnumerable<StatisticsDto>> GetStatisticsAsync()
    {
        var projectLevelStatistics = await repository.GetProjectLevelStatisticsAsync();

        return projectLevelStatistics
            .Select(x => new StatisticsDto
            {
                ProjectName = x.Key,
                CompletedTasks = x.Value.CompletedTasks,
                OverdueTasks = x.Value.OverdueTasks,
                TotalTasks = x.Value.TotalTasks
            });
    }

    public async Task<FlowTaskDto> AddAsync(FlowTaskDto taskDto)
    {
        var newTask = await repository.AddAsync(new FlowTask
        {
            Id =  taskDto.Id,
            Title = taskDto.Title,
            Description = taskDto.Description,
            PriorityId = taskDto.PriorityId,
            StatusId = taskDto.StatusId
        });
        
        await repository.SaveChangesAsync();

        return new FlowTaskDto
        {
            Id = newTask.Id,
            Title = newTask.Title,
            Description = newTask.Description,
            PriorityId = taskDto.PriorityId,
            StatusId = taskDto.StatusId
        };
    }

    public async Task<bool> UpdateAsync(int id, FlowTaskDto taskDto)
    {
        var existingFlowTask = await repository.FindAsync(id);

        if (existingFlowTask == null)
        {
            throw new KeyNotFoundException($"FlowTask with id {id} not found");
        }

        existingFlowTask.Title = taskDto.Title;
        existingFlowTask.Description = taskDto.Description;
        existingFlowTask.DueDate = taskDto.DueDate;
        existingFlowTask.StatusId = taskDto.StatusId;
        existingFlowTask.PriorityId = taskDto.PriorityId;
        existingFlowTask.AssignedUserId = taskDto.AssignedUserId;
        existingFlowTask.ProjectId = taskDto.ProjectId;

        repository.Update(existingFlowTask);

        return await repository.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await repository.FindAsync(id);

        if (task == null)
        {
            throw new KeyNotFoundException($"FlowTask with id {id} not found");
        }

        repository.Delete(task);

        return await repository.SaveChangesAsync() > 0;
    }
}