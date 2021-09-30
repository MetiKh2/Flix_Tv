using Flix_Tv.Application.DTOs.Permission.CreateRole;
using Flix_Tv.Application.DTOs.Roles.Admin;
using Flix_Tv.Application.DTOs.Roles.Admin.Users;
using Flix_Tv.Domain.Entites.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.Services.Interfaces
{
  public  interface IPermissionService
    {
        Task<List<GetPermissionForRolesDto>> GetPermissionForRoles();
        Task<Role> CreateRole(string title);
        Task CreateRolePermissions(List<long> selectedPermissions,long roleId);
        Task<bool> IsRoleTitleExists(string title);
        Task<Tuple<List<GetRolesDto>,int>> GetRolesInAdmin(string filter="");
        Task<Role> GetRoleById(long id);
        Task UpdateRole(Role role);
        Task<bool> DeleteRole(long id);
        Task<List<RolePermission>> GetRolePermissionByRoleId(long roleId);
        Task EditRole(EditRoleDto dto);
        Task EditRolePermissions(long roleId,List<long> selectedPermissions);
       bool CheckPermission(string userName,long permissionId);
        Task<List<GetRolesForUsersDto>> GetRolesForUsers();
        Task CreateUserRoles(long userId,List<long> SelectedRoles);
        Task<List<long>> GetUserRolesIdByUserId(long userId);
        Task<List<Domain.Entites.Users.UserRole>> GetUserRolesByUserId(long userId);
        Task EditUserRoles(long userId, List<long> SelectedRoles);
        Task<List<long>> GetRolesIdByPermissionId(long permissionId);
        Task<bool> CheckPermissionForPostActions(string userName,int permissionId);
    }
}
