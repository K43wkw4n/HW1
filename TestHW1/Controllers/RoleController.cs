using Azure.Core;
using TestHW1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestHW1.Models;
using TestHW1.Dtos;
using TestHW1.Service.IService;

namespace TestHW1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly Context _context;

        public RoleController(IRoleService roleService, Context context)
        {
            _roleService = roleService;
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRole()
            => Ok(await _roleService.GetRoleAsync());

        [HttpPost("[action]")]
        public async Task<ActionResult<Role>> CreateRole(CreateRoleDto createRoleDto)
            => Ok(await _roleService.CreateRoleAsync(createRoleDto));

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto updateRole)
            => Ok(await _roleService.UpdateRoleAsync(updateRole));

        [HttpDelete("[action]")]
        public async Task<IActionResult> Remove(int id)
            => Ok(await _roleService.DeleteRoleAsync(id));


















        //[HttpGet("[action]")]
        //public async Task<IActionResult> TestGetRole(int id)
        //{
        //    var role = await _context.Roles.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == id);

        //    return Ok(role.Users);
        //}
    }
}

//BadRequest(new ProblemDetails { Title = "Problem about remove", Status = StatusCodes.Status404NotFound });