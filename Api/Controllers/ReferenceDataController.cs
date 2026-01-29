using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[Authorize]
public class ReferenceDataController(
    IReferenceDataService referenceDataService) : BaseController
{
    [SwaggerOperation(Summary = "Gets all priorities")]
    [HttpGet]
    [Route("priorities")]
    public async Task<IActionResult> GetPriorities()
    {
        var priorities = await referenceDataService.GetPrioritiesAsync();
        
        return Ok(priorities);
    }
    
    [SwaggerOperation(Summary = "Gets all statuses")]
    [HttpGet]
    [Route("statuses")]
    public async Task<IActionResult> GetStatuses()
    {
        var statuses = await referenceDataService.GetStatusesAsync();
        
        return Ok(statuses);
    }  
}