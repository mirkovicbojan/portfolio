using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.DTO_models;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult Save(Project obj)
        {
            return Ok(_projectService.Save(obj));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_projectService.GetAll());
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Project project = _projectService.GetOne(id);
            if (project == null)
            {
                return BadRequest("Client not found");
            }
            return Ok(ProjectDTO.ToProjectDTO(project));
        }

        [Authorize]
        [HttpPut]
        public ActionResult<Project> UpdateProject(Project request)
        {
            var project = _projectService.GetOne(request.projectID);
            if (project == null)
            {
                return BadRequest("Client not found");
            }
            return _projectService.UpdateOne(request);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _projectService.GetOne(id);
            if (project == null)
            {
                return BadRequest("Client not found");
            }
            _projectService.DeleteOne(project);
            return Ok("Project deleted successfully.");
        }
    }
}