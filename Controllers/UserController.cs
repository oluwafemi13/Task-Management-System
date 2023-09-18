using Management.Application.Models;
using Management.Core.Entities;
using Management.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserservice _service;

        public UserController(IUserservice service)
        {
            _service = service;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser(User user)
        {
            try
            {
                var create = await _service.CreateUser(user);
                return Ok(create);
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser(int Id, User user)
        {
            var update = await _service.Update(user, Id);
            return Ok(update);
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser([FromQuery] int Id)
        {
            var delete = await _service.DeleteUser(Id);
            return Ok(delete);
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult> GetUsers([FromQuery] RequestParameters parameters)
        {
            var fetchAll = await _service.GetAll(parameters);
            return Ok(fetchAll);
        }
    }
}
