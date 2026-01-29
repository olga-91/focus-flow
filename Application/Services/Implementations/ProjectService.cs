using Application.Dtos;
using Application.Exceptions;
using Application.Services.Interfaces;
using Domain.Model;
using Domain.Repositories;

namespace Application.Services.Implementations;

public class ProjectService(
    IProjectRepository repository) : IProjectService
{
    public async Task<IEnumerable<ProjectDto>> GetAllAsync()
    {
        var projects = await repository.GetAllAsync();

        return projects.Select(x => new ProjectDto
        {
            Id = x.Id, Name = x.Name, Description = x.Description
        });
    }

    public async Task<ProjectDto?> GetByIdAsync(int id)
    {
        var project = await repository.FindAsync(id);

        if (project == null)
        {
            throw new KeyNotFoundException($"Project with id {id} not found");
        }

        return new ProjectDto
        {
            Id = project.Id, Name = project.Name, Description = project.Description
        };
    }

    public async Task<ProjectDto> AddAsync(ProjectDto projectDto)
    {
        var newProject = new Project
        {
            Name = projectDto.Name, Description =  projectDto.Description
        };

        await repository.AddAsync(newProject);
        await repository.SaveChangesAsync();

        return new ProjectDto
        {
            Id = newProject.Id, Name = newProject.Name, Description = newProject.Description
        };
    }

    public async Task<bool> UpdateAsync(int id, ProjectDto projectDto)
    {
        var existingProject = await repository.FindAsync(id);

        if (existingProject == null)
        {
            throw new KeyNotFoundException($"Project with id {id} not found");
        }

        existingProject.Name = projectDto.Name;
        existingProject.Description = projectDto.Description;

        repository.Update(existingProject);

        return await repository.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await repository.FindAsync(id);


        if (project == null)
        {
            throw new KeyNotFoundException($"Project with id {id} not found");
        }

        var isInUse = await repository.IsInUseAsync(id);
        
        if (isInUse)
        {
            throw new DataConflictException("Project is in use and can't be deleted");
        }
        
        repository.Delete(project);

        return await repository.SaveChangesAsync() > 0;
    }
}