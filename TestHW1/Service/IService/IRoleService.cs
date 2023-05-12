using TestHW1.Dtos;

namespace TestHW1.Service.IService
{
    public interface IRoleService
    {
        Task<List<Object>> GetRoleAsync();
        Task<object> CreateRoleAsync(CreateRoleDto createRoleDto);
        Task<object> UpdateRoleAsync(UpdateRoleDto updateRole);
        Task<object> DeleteRoleAsync(int id);

    }
}
