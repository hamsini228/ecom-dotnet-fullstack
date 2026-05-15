using eCommerce.Application.Contracts;
using eCommerce.Domain;

namespace eCommerce.Application.Services;

public class RoleService
{
    private readonly ICommonRepository<Role> _roleRepository;

    public RoleService(ICommonRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<Role>> GetRolesAsync()
    {
        return await _roleRepository.GetAllAsync();
    }
    public async Task<Role> GetRoleDetailsAsync(int id)
    {
        return await _roleRepository.GetByIdAsync(id);
    }
    public async Task<int> CreateRole(Role Role)
    {
        return await _roleRepository.AddAsync(Role);
    }
    public async Task<int> UpdateRole(Role Role)
    {
        return await _roleRepository.UpdateAsync(Role);
    }
    public async Task<int> DeleteRole(int id)
    {
        return await _roleRepository.DeleteAsync(id);
    }
}
