using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestHW1.Data;
using TestHW1.Dtos;
using TestHW1.Models;
using TestHW1.Service.IService;

namespace TestHW1.Service
{
    public class AuthService : ControllerBase, IAuthService
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(Context context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<object>> GetUserRolesAsync()
        {
            var result = await _context.Users.Include(x => x.Roles).ToListAsync();

            List<Object> users = new List<Object>();
            foreach (var user in result)
            {
                users.Add(new { user, user.Roles });
            }

            return users;
        }

        public async Task<object> RegisterAsync(RegisterDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == request.Role);

            if (user != null || role == null)
                return NotFound();

            string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var User = new User
            {
                UserName = request.Username,
                Password = passwordHash,
                Roles = new List<Role> { role }
            };

            await _context.Users.AddAsync(User);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }

        public async Task<object> LoginAsunc(LoginDto request)
        {
            var user = await _context.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.UserName.Equals(request.Username));

            if (user == null) { return BadRequest("UserName Wrong"); }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) { return BadRequest("Password Wrong"); }

            string token = CreateToken(user);

            return token;
        }

        public async Task<object> AddRoleAsync(AddRoleDto request)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == request.UserId);
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId);

            if (user == null || role == null)
                return NotFound();

            user.Roles.Add(role);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private string CreateToken(User user)
        { 
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                
                };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
         
        public Object CheckAutherizedAsync()
        {
            var username = string.Empty;
            var role = string.Empty;

            if (_httpContextAccessor.HttpContext != null)
            {
                username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            
            //var roleClaims = User.FindAll(ClaimTypes.Role);
            //var roles = roleClaims.Select(x => x.Value).ToList();

            return new { username };
        }


    }
}
