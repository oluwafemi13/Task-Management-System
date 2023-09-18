using Management.Application.DTO;
using Management.Application.Models;
using Management.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Task_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;


        public TaskController(ITaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }


        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto task)
        {
            try
            {
                
                    await _taskRepo.CreateAsync(task);
                    return Ok("Task Created"); 
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPut("UpdateTask")]
        public async Task<ActionResult> UpdateTask([FromBody]TaskDto task, int Id)
        {
            task.TaskId = Id;
            var update =await _taskRepo.UpdateTask(Id, task);
            return Ok(update);
        }

        [HttpPut("AssignTaskToProject")]
        [ProducesResponseType(200, StatusCode = (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Management.Core.Entities.Task>> AssignTaskToProject(int taskId, int newProjectId)
        {
            var result = await _taskRepo.AssignTaskToProject(taskId, newProjectId);
            return Ok(result);
        }

        [HttpDelete("DeleteTask")]
        public async Task<ActionResult> DeleteTask([FromQuery]int Id)
        {
            var result = await _taskRepo.DeleteTask(Id);
            return Ok(result);

        }

        [HttpGet("GetAllTask")]
        public async Task<ActionResult<IEnumerable<Management.Core.Entities.Task>>> GetAllTask([FromQuery] int pageStart, int pageSize)
        {
            var result = await _taskRepo.GetAllTask(pageStart, pageSize);
            return Ok(result);
        }


        [HttpGet("GetTaskByUser/User")]
        public async Task<ActionResult<IEnumerable<Management.Core.Entities.Task>>> GetTaskByUser([FromQuery] int pageStart, int pageSize, int UserId)
        {
            var result = await _taskRepo.GetTaskByUser(UserId, pageStart, pageSize);
            return Ok(result);
        }

        [HttpGet("GetTaskByTitle")]
        [ProducesResponseType(200,StatusCode = (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Management.Core.Entities.Task>>> GetTaskByTitle([FromQuery] string title, int userId)
        {
            var result = await _taskRepo.GetTaskByTitle(title, userId);
            return Ok(result);
        }

        [HttpGet("GetTaskByStatus")]
        [ProducesResponseType(200, StatusCode = (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Management.Core.Entities.Task>>> GetTaskByStatus([FromQuery] string status)
        {
            var result = await _taskRepo.GetTaskByStatus(status);
            return Ok(result);
        }

        [HttpGet("GetTaskForTheWeek")]
        [ProducesResponseType(200, StatusCode = (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Management.Core.Entities.Task>>> GetTaskForTheWeek()
        {

           
            var result = await _taskRepo.GetTaskForWeek();
            return Ok(result);
        }

       

    }
}
