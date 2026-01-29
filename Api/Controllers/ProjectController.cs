using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[Authorize]
public class ProjectController(
    IProjectService projectService) : BaseController
{
    [SwaggerOperation(Summary = "Get project by id")]
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var project = await projectService.GetByIdAsync(id);
        
        return Ok(project);
    }
    
    [SwaggerOperation(Summary = "Gets all projects")]
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        var projects = await projectService.GetAllAsync();
        
        return Ok(projects);
    }
    
    [SwaggerOperation(Summary = "Creates a new project")]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProjectDto projectDto)
    {
        var project = await projectService.AddAsync(projectDto);
        
        return Ok(project);
    }
    
    [SwaggerOperation(Summary = "Updates a new project")]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] ProjectDto projectDto)
    {
        var project = await projectService.UpdateAsync(id, projectDto);
        
        return Ok(project);
    }
    
    [SwaggerOperation(Summary = "Deletes a project")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await projectService.DeleteAsync(id);
        
        return Ok(project);
    }
}