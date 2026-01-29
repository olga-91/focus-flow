using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Repositories;

namespace Application.Services.Implementations;

public class ReferenceDataService(
    IPriorityRepository priorityRepository,
    IStatusRepository statusRepository) : IReferenceDataService
{
    public async Task<IEnumerable<ReferenceDataDto>> GetPrioritiesAsync()
    {
        var projects = await priorityRepository.GetAllAsync();

        return projects.Select(x => new ReferenceDataDto
        {
            Id = x.Id, Name = x.Name
        });
    }
    
    public async Task<IEnumerable<ReferenceDataDto>> GetStatusesAsync()
    {
        var statuses = await statusRepository.GetAllAsync();

        return statuses.Select(x => new ReferenceDataDto
        {
            Id = x.Id, Name = x.Name
        });
    }
}