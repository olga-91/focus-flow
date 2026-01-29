using Application.Dtos;

namespace Application.Services.Interfaces;

public interface IFlowTaskService
{
    Task<IEnumerable<FlowTaskDto>> FilterAsync(FilterDto filterDto);
    Task<FlowTaskDto?> GetByIdAsync(int id);
    Task<FlowTaskDto> AddAsync(FlowTaskDto taskDto);
    Task<bool> UpdateAsync(int id, FlowTaskDto taskDto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<StatisticsDto>> GetStatisticsAsync();
}