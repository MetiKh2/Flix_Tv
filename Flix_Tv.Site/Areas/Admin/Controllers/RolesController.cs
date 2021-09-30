using Flix_Tv.Application.DTOs.Roles.Admin;
using Flix_Tv.Application.Security;
using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Common.Convertors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly IPermissionService _permissionService;
        public RolesController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [Route("Admin/Roles")]
        [PermissionChecker(20)]
        public async Task<IActionResult> Index(string filter="")
        {
            ViewBag.filter = filter;
            ViewBag.Permissions = await _permissionService.GetPermissionForRoles();
            return View(await _permissionService.GetRolesInAdmin(filter));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleTitle, List<long> selectedPermissions)
        {
            if (!User.Identity.IsAuthenticated) return NotFound();
            if (!await _permissionService.CheckPermissionForPostActions(User.Identity.Name, 21)) return Redirect("/Login");
            try
            {
                if (string.IsNullOrEmpty(roleTitle)||roleTitle.Length>50)
                {
                    return Json(false);
                }
                if (await _permissionService.IsRoleTitleExists(roleTitle))
                {
                    return Json("RoleTitleExists");
                }
                var role = await _permissionService.CreateRole(roleTitle);
                await _permissionService.CreateRolePermissions(selectedPermissions, role.Id);
                return Json(new GetRolesDto { CreateDate = role.CraeteDate.ToShamsi(), Id = role.Id, Title = role.RoleTitle });
            }
            catch
            {
                return Json(false);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(long Id)
        {
            if (!User.Identity.IsAuthenticated) return NotFound();
            if (!await _permissionService.CheckPermissionForPostActions(User.Identity.Name, 22)) return Redirect("/Login");
            return Json(await _permissionService.DeleteRole(Id));
        }

        [Route("Admin/EditRole/{id}")]
      //  [PermissionChecker(23)]
        public async Task<IActionResult> EditRole(long id)
        {
            var role = await _permissionService.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }
            var rolePermissions = await _permissionService.GetRolePermissionByRoleId(id);
            ViewBag.RolePermissions = rolePermissions.Select(p => p.PermissionId).ToList();
            ViewBag.Permissions = await _permissionService.GetPermissionForRoles();
            return View(new EditRoleDto
            {
                Id = role.Id,
                LastTitle = role.RoleTitle,
                Title=role.RoleTitle,
            
            });
        }

        [Route("Admin/EditRole/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleDto dto ,List<long> selectedPermissions)
        {
         //   if (!User.Identity.IsAuthenticated) return NotFound();
         //   if (!await _permissionService.CheckPermissionForPostActions(User.Identity.Name, 23)) return Redirect("/Login");

            if (ModelState.IsValid==false)
            {
                var rolePermissions = await _permissionService.GetRolePermissionByRoleId(dto.Id);
                ViewBag.RolePermissions = rolePermissions.Select(p => p.PermissionId).ToList();
                ViewBag.Permissions = await _permissionService.GetPermissionForRoles();
                return View(dto);
            }
            if (dto.LastTitle!=dto.Title)
            {
                if (await _permissionService.IsRoleTitleExists(dto.Title))
                {
                    var rolePermissions = await _permissionService.GetRolePermissionByRoleId(dto.Id);
                    ViewBag.RolePermissions = rolePermissions.Select(p => p.PermissionId).ToList();
                    ViewBag.Permissions = await _permissionService.GetPermissionForRoles();
                    ModelState.AddModelError("Title","نام نقش تکراری می باشد");
                    return View(dto);
                }
            }
           await _permissionService.EditRole(dto);
            await _permissionService.EditRolePermissions(dto.Id,selectedPermissions);
            return Redirect("/Admin/Roles");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(long Id)
        {
            if (!User.Identity.IsAuthenticated) return NotFound();
            if (!await _permissionService.CheckPermissionForPostActions(User.Identity.Name, 23)) return Redirect("/Login");

            var role =await _permissionService.GetRoleById(Id);
            if (role==null)
            {
                return NotFound();
            }
            role.IsActive = !role.IsActive;
            role.UpdateDate = DateTime.Now;
           await _permissionService.UpdateRole(role);
            return Json(new ChangeRoleStatusResultDto {Id=role.Id,IsActive=role.IsActive });
        }
    }
}
