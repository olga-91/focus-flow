using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[Authorize]
public class FlowTaskController(
    IFlowTaskService taskService) : BaseController
{
    [SwaggerOperation(Summary = "Get task by id")]
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var task = await taskService.GetByIdAsync(id);
        
        return Ok(task);
    }
    
    [SwaggerOperation(Summary = "Create new task")]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] FlowTaskDto taskDto)
    {
        var task = await taskService.AddAsync(taskDto);
        
        return Ok(task);
    }
    
    [SwaggerOperation(Summary = "Filter tasks by project name, status and priority")]
    [HttpPost]
    [Route("filter")]
    public async Task<IActionResult> Filter([FromBody] FilterDto filterDto)
    {
        var tasks = await taskService.FilterAsync(filterDto);
        
        return Ok(tasks);
    }
    
    [SwaggerOperation(Summary = "Updates a task")]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] FlowTaskDto flowTaskDto)
    {
        var task = await taskService.UpdateAsync(id, flowTaskDto);
        
        return Ok(task);
    }
    
    [SwaggerOperation(Summary = "Deletes a task")]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
       await taskService.DeleteAsync(id);
        
        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("statistics")]
    public async Task<IActionResult> GetStatistics()
    {
        var statistics = await taskService.GetStatisticsAsync();
        
        return Ok(statistics);
    }
}