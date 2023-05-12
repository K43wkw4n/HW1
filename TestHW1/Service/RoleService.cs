using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestHW1.Data;
using TestHW1.Dtos;
using TestHW1.Models;
using TestHW1.Service.IService;

namespace TestHW1.Service
{
    public class RoleService : ControllerBase, IRoleService
    {
        private readonly Context _context;

        public RoleService(Context context)
        {
            _context = context;
        }

        public async Task<List<object>> GetRoleAsync()
        {
            var result = await _context.Roles.ToListAsync();

            List<Object> roles = new List<Object>();

            foreach (var role in result)
            {
                roles.Add(new { role });
            }

            return roles;
        }

        public async Task<object> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            var result = await _context.Roles.FirstOrDefaultAsync(x => x.Name == createRoleDto.Name);

            if (result != null) return StatusCode(StatusCodes.Status400BadRequest);

            var role = new Role()
            {
                Name = createRoleDto.Name,
            };

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<object> UpdateRoleAsync(UpdateRoleDto updateRole)
        {
            var result = await _context.Roles.FirstOrDefaultAsync(x => x.Id == updateRole.Id);
            if (result == null || result.Name == updateRole.Name) return NotFound();
            
            result.Name = updateRole.Name;
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }

        public async Task<object> DeleteRoleAsync(int id)
        {
            var result = _context.Roles.FirstOrDefault(x => x.Id == id);
            var role = await _context.Roles.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == id);

            _context.Roles.Remove(result);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);

        }


    }
}
