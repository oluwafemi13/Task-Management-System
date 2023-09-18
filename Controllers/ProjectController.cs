using Management.Application.Models;
using Management.Core.Entities;
using Management.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        [HttpPost("CreateProject")]
        public async Task<ActionResult> CreateProject(Project project)
        {
            try
            {
                var create = await _service.CreateProject(project);
                return Ok(create);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        [HttpPut("UpdateProject")]
        public async Task<ActionResult> UpdateProject(int Id, Project project)
        {
           var update =  await _service.Update(project, Id);
            return Ok(update);
        }

        [HttpDelete("DeleteProject")]
        public async Task<ActionResult> DeleteProject([FromQuery]int Id)
        {
            var delete = await _service.DeleteProject(Id);
            return Ok(delete);
        }

        [HttpGet("GetAllProjects")]
        public async Task<ActionResult> GetProjects([FromQuery] RequestParameters parameters)
        {
            var fetchAll = await _service.GetAll(parameters);
            return Ok(fetchAll);
        }

        //[HttpGet("GetProjectsByName")]
        //public async Task<ActionResult> GetProjectsByName([FromQuery] string name)
        //{
        //    var result = await _service.GetProjectByName(name);
        //    return Ok(result);
        //}




    }
}
