
using Asp.Versioning;
using DataLayer.Irepo;
using DataLayer.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Drawing;
using WebApi.Models;
namespace WebApi.Controllers.v1
{
    [ApiController] //This enables auto validation of model and if validation fails return 400;
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class UserController:ControllerBase
    {
        private readonly Iuser _repo;
        public UserController(Iuser _repo)
        {
            this._repo = _repo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new
            {
                message = "This service is no longer avilabe in  V1"
            });
        }
        //[HttpGet] //now if i donot mention anything then it will expect data from query string that like /api/User/GetUser?2
        [HttpGet("{id}")] //in this case itr will for route /api/User/GetUser/2
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                UserModel user = await _repo.GetUserById(id);
                return Ok(user);
            }
            catch(Exception e)
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel user)
        {
            try
            {
                bool res = await _repo.CreateUser(user);
                if (res)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500, new
                    {
                        message = "There is some server side issue"
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = e.Message
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UserModel user)
        {
            try
            {
                bool res = await _repo.UpdateUser(user);
                if (res)
                {
                    return NoContent();
                }
                return StatusCode(500, new
                {
                    message = "There is some server side issue"
                });
            }   
            catch(Exception e)
            {
                return BadRequest(new
                {
                    message = e.Message
                });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool res =await  _repo.DeleteUser(id);
                if (res) return Ok(new
                {
                    message = "User deleted"
                });
                else return StatusCode(500, new
                {
                    message = "There is some server side issue"
                });
            }
            catch(Exception e)
            {
                return NotFound(new
                {
                    message = e.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            bool res = await _repo.ValidateUser(req.email, req.password);
            if (res)
            {
                UserModel user = await _repo.GetUserByEmail(req.email);
                return Ok(user);
            }
            else
            {
                return Unauthorized(new
                {
                    message = "Invaid email or password"

                });
            }
        }
    }
}
