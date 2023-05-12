using Azure.Core;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestHW1.Data;
using TestHW1.Dtos;
using TestHW1.Models;
using TestHW1.Service;
using TestHW1.Service.IService;

namespace TestHW1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserRole() 
            => Ok(await _authService.GetUserRolesAsync());

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDto request) 
            => Ok(await _authService.RegisterAsync(request)); 
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto request) 
            => Ok(await _authService.LoginAsunc(request)); 

        [HttpPost("[action]")]
        public async Task<IActionResult> AddRole(AddRoleDto request)
            => Ok(await _authService.AddRoleAsync(request));

        [HttpGet("[action]"), Authorize]
        public IActionResult CheckAutherized() 
            => Ok(_authService.CheckAutherizedAsync());

















        //[HttpGet("GetToken"), Authorize]
        //public async Task<IActionResult> GetToken()
        //{
        //    var accessToken = await HttpContext.GetTokenAsync("access_token");
        //    return Ok(accessToken);
        //}
         


    }
}
