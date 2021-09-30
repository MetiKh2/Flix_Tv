using Flix_Tv.Application.DatabaseInterfaces;
using Flix_Tv.Application.DTOs.Permission.CreateRole;
using Flix_Tv.Application.DTOs.Roles.Admin;
using Flix_Tv.Application.DTOs.Roles.Admin.Users;
using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Common.Convertors;
using Flix_Tv.Domain.Entites.Roles;
using Flix_Tv.Domain.Entites.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IFlixTvContext _context;
        private readonly IUserService _userService;
        public PermissionService(IFlixTvContext context,IUserService userService)
        {
            _userService = userService;
            _context = context;
        }

        public bool CheckPermission(string userName, long permissionId)
        {
            var userId = _context.Users.FirstOrDefault(p=>p.UserName==userName).Id;
            if (userId == 0)
                return false;
            var userRoles = _context.UserRoles.Include(p=>p.Role).Where(p => p.UserId == userId&&p.Role.IsActive).Select(p => p.RoleId).ToList();
            if (!userRoles.Any())
            {
                return false;
            }

            var rolePermission = _context.RolePermissions.Include(p=>p.Role).Where(p => p.PermissionId == permissionId&&p.Role.IsActive).Select(p => p.RoleId).ToList();
            return rolePermission.Any(p=>userRoles.Contains(p));
        }

        public async Task<bool> CheckPermissionForPostActions(string userName,int permissionId)
        {
            var userId =await _userService.GetUserIdByUserName(userName) ;
            if (userId == 0)
                return false;
            var userRoles =await _context.UserRoles.Include(p => p.Role).Where(p => p.UserId == userId && p.Role.IsActive).Select(p => p.RoleId).ToListAsync();
            if (!userRoles.Any())
            {
                return false;
            }

            var rolePermission =await _context.RolePermissions.Include(p => p.Role).Where(p => p.PermissionId == permissionId && p.Role.IsActive).Select(p => p.RoleId).ToListAsync();
            return rolePermission.Any(p => userRoles.Contains(p));

        }

        public async Task<Role> CreateRole(string title)
        {
            var newRole = new Role
            {
                CraeteDate = DateTime.Now,
                RoleTitle = title,
            };
            await _context.Roles.AddAsync(newRole);
            await _context.SaveChangesAsync();
            return newRole;
        }

        public async Task CreateRolePermissions(List<long> selectedPermissions, long roleId)
        {
            foreach (var item in selectedPermissions)
            {
                await _context.RolePermissions.AddAsync(new RolePermission
                {
                    CraeteDate = DateTime.Now,
                    PermissionId = item,
                    RoleId = roleId,
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task CreateUserRoles(long userId, List<long> selectedRoles)
        {
            foreach (var item in selectedRoles)
            {
                await _context.UserRoles.AddAsync(new Domain.Entites.Users.UserRole { 
                CraeteDate=DateTime.Now,
                RoleId=item,
                UserId=userId,
                });
            }
           await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteRole(long id)
        {
            try
            {
                var role = await GetRoleById(id);
                if (role != null)
                {
                    role.IsRemoved = true;
                    var rolePermissions = await GetRolePermissionByRoleId(id);
                    foreach (var item in rolePermissions)
                    {
                        item.IsRemoved = true;
                        _context.RolePermissions.Update(item);
                    }
                    await UpdateRole(role);
                }
                return true;
            }
            catch
            {
                return false;
                throw;
            }
        }

        public async Task EditRole(EditRoleDto dto)
        {
            var role = await GetRoleById(dto.Id);
            role.UpdateDate = DateTime.Now;
            role.RoleTitle = dto.Title;
            await UpdateRole(role);
        }

        public async Task EditRolePermissions(long roleId, List<long> selectedPermissions)
        {
            var lastRolePermissions = await GetRolePermissionByRoleId(roleId);
            foreach (var item in lastRolePermissions)
            {
                item.IsRemoved = true;
                item.RemoveDate = DateTime.Now;

            }
            foreach (var item in selectedPermissions)
            {
                await _context.RolePermissions.AddAsync(new RolePermission
                {
                    CraeteDate = DateTime.Now
                ,
                    PermissionId = item,
                    RoleId = roleId,
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task EditUserRoles(long userId, List<long> SelectedRoles)
        {
            var lastUserRoles =await GetUserRolesByUserId(userId);
            foreach (var item in lastUserRoles)
            {
                item.IsRemoved = true;
                item.RemoveDate = DateTime.Now;
            }
           await CreateUserRoles(userId,SelectedRoles);
        }

        public async Task<List<GetPermissionForRolesDto>> GetPermissionForRoles()
        {
            return await _context.Permissions.Select(p => new GetPermissionForRolesDto
            {
                Id = p.Id,
                ParentId = p.ParentId,
                Title = p.Title
            }).ToListAsync();
        }

        public async Task<Role> GetRoleById(long id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<List<RolePermission>> GetRolePermissionByRoleId(long roleId)
        {
            return await _context.RolePermissions.Where(p => p.RoleId == roleId).ToListAsync();
        }

        public async Task<List<GetRolesForUsersDto>> GetRolesForUsers()
        {
            return await _context.Roles.Select(p => new GetRolesForUsersDto { Id = p.Id, Title = p.RoleTitle }).ToListAsync();
        }

        public async  Task<List<long>> GetRolesIdByPermissionId(long permissionId)
        {
            return await _context.RolePermissions.Where(p => p.PermissionId == permissionId).Select(p => p.RoleId).ToListAsync();
        }

        public async Task<Tuple<List<GetRolesDto>,int>> GetRolesInAdmin(string filter = "")
        {
            var roles = _context.Roles.OrderByDescending(p => p.CraeteDate).AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                roles = roles.Where(p => p.RoleTitle.Contains(filter)).AsQueryable();
            }
            return Tuple.Create(await roles.Select(p => new GetRolesDto
            {
                Id = p.Id,
                CreateDate = p.CraeteDate.ToShamsi(),
                Title = p.RoleTitle,
                IsActive = p.IsActive
            }).ToListAsync(),roles.Count());

        }

        public async Task<List<UserRole>> GetUserRolesByUserId(long userId)
        {
            return await _context.UserRoles.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<List<long>> GetUserRolesIdByUserId(long userId)
        {
            return await _context.UserRoles.Where(p => p.UserId == userId).Select(p => p.RoleId).ToListAsync() ;
        }

        public async Task<bool> IsRoleTitleExists(string title)
        {
            return await _context.Roles.AnyAsync(p => p.RoleTitle == title);
        }

        public async Task UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
    }
}
