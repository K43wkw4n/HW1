using TestHW1.Dtos;

namespace TestHW1.Service.IService
{
    public interface IAuthService
    {
        Task<List<Object>> GetUserRolesAsync();
        Task<Object> RegisterAsync(RegisterDto request);
        Task<Object> LoginAsunc(LoginDto request);
        Task<Object> AddRoleAsync(AddRoleDto request);
        Object CheckAutherizedAsync();
    }
}
