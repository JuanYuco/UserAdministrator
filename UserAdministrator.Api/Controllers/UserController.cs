using Microsoft.AspNetCore.Mvc;
using UserAdministrator.Api.DTOS;
using UserAdministrator.Api.Services.Interfaces;

namespace UserAdministrator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAllAsync();
            if (!response.Successful)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateUserRequestDTO request)
        {
            var response = await _userService.CreateAsync(request);
            if (!response.Successful)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateUserRequestDTO request)
        {
            var response = await _userService.UpdateAsync(request);
            if (!response.Successful)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _userService.DeleteAsync(id);
            if (!response.Successful)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }
    }
}
