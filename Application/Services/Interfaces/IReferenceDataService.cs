using Application.Dtos;

namespace Application.Services.Interfaces;

public interface IReferenceDataService
{
    Task<IEnumerable<ReferenceDataDto>> GetPrioritiesAsync();
    Task<IEnumerable<ReferenceDataDto>> GetStatusesAsync();
}