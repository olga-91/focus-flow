using Application.Dtos;

namespace Application.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetAllAsync();
    Task<ProjectDto?> GetByIdAsync(int id);
    Task<ProjectDto> AddAsync(ProjectDto projectDto);
    Task<bool> UpdateAsync(int id, ProjectDto projectDto);
    Task<bool> DeleteAsync(int id);
}