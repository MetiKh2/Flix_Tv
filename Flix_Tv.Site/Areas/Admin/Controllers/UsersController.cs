using Flix_Tv.Application.DTOs.User.Admin;
using Flix_Tv.Application.Security;
using Flix_Tv.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public UsersController(IUserService userService,IPermissionService permissionService)
        {
            _permissionService = permissionService;
            _userService = userService;
        }
        [Route("Admin/users")]
        [PermissionChecker(16)]
        public async Task<IActionResult> Index(int pageId=1,string filter="",string sort="date")
        {
            ViewBag.filter = filter;
            ViewBag.sort = sort;
            ViewBag.pageId = pageId;
            return View(await _userService.GetUsersInAdmin(pageId,filter,sort));
        }

        [Route("Admin/CreateUser")]
        [PermissionChecker(17)]
        public async Task<IActionResult> CreateUser()
        {
            ViewBag.Roles =await _permissionService.GetRolesForUsers();
            return View();
        }
        //[PermissionChecker(17)]
        [Route("Admin/CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto dto ,List<long> selectedRoles)
        {
            if (!User.Identity.IsAuthenticated) return NotFound();
            if (!await _permissionService.CheckPermissionForPostActions(User.Identity.Name, 17)) return Redirect("/Login");

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _permissionService.GetRolesForUsers();
                return View(dto);
            }
            if (await _userService.IsUserNameExist(dto.UserName))
            {
                ViewBag.Roles = _permissionService.GetRolesForUsers();
                ModelState.AddModelError("UserName", "این نام کاربری قبلا مورد استفاده قرار گرفته");
                return View(dto);
            }
            if (await _userService.IsEmailExist(dto.Email))
            {
                ViewBag.Roles = _permissionService.GetRolesForUsers();
                ModelState.AddModelError("Email", "این ایمیل قبلا مورد استفاده قرار گرفته");
                return View(dto);
            }
            
         var userId=  await _userService.CreateUserInAdmin(dto);
            await _permissionService.CreateUserRoles(userId,selectedRoles);
            return Redirect("/admin/users");
        }
        [PermissionChecker(19)]
        [Route("Admin/EditUser/{id}")]
        public async Task<IActionResult> EditUser(long id)
        {
            var user = await _userService.GetUserById(id);
            if (user==null)
            {
                return NotFound();
            }
            ViewBag.Roles = await _permissionService.GetRolesForUsers();
            ViewBag.UserRoles = await _permissionService.GetUserRolesIdByUserId(id);
            return View(new EditUserDto { 
            FullName=user.FullName,
            Email=user.Email,
            UserName=user.UserName,
            LastAvatar=user.Avatar,
            LastEmail=user.Email,
            LastUserName=user.UserName,
            Id=user.Id,
            IsActive=user.IsActive
            });
        }
       
        [Route("Admin/EditUser/{id}")]
        [HttpPost]
        //[PermissionChecker(19)]
        public async Task<IActionResult> EditUser(EditUserDto dto, List<long> selectedRoles)
        {
            if (!User.Identity.IsAuthenticated) return NotFound();
            if (!await _permissionService.CheckPermissionForPostActions(User.Identity.Name, 19)) return Redirect("/Login");

            if (!User.Identity.IsAuthenticated) return NotFound();
            if (!ModelState.IsValid)
            {
                ViewBag.UserRoles = await _permissionService.GetUserRolesIdByUserId(dto.Id);
                ViewBag.Roles = await _permissionService.GetRolesForUsers();
                return View(dto);
            }
            if (dto.LastUserName != dto.UserName)
            {
                if (await _userService.IsUserNameExist(dto.UserName))
                {
                    ViewBag.UserRoles = await _permissionService.GetUserRolesIdByUserId(dto.Id);
                    ViewBag.Roles = await _permissionService.GetRolesForUsers();
                    ModelState.AddModelError("UserName", "این نام کاربری قبلا مورد استفاده قرار گرفته");
                    return View(dto);
                }
            }
            if (dto.LastEmail != dto.Email)
            {
                if (await _userService.IsEmailExist(dto.Email))
                {
                    ViewBag.UserRoles = await _permissionService.GetUserRolesIdByUserId(dto.Id);
                    ViewBag.Roles = await _permissionService.GetRolesForUsers();
                    ModelState.AddModelError("Email", "این ایمیل قبلا مورد استفاده قرار گرفته");
                    return View(dto);
                }
            }
          await  _userService.EditUserInAdmin(dto);
            await _permissionService.EditUserRoles(dto.Id,selectedRoles);
            return Redirect("/Admin/Users");
        }
       // [PermissionChecker(18)]
        [Route("Admin/DeleteUser/{id}")]
        public async Task< IActionResult >DeleteUser(long id)
        {
            if (!User.Identity.IsAuthenticated) return NotFound();
            if (!await _permissionService.CheckPermissionForPostActions(User.Identity.Name, 18)) return Redirect("/Login");

           
            return Json(await _userService.DeleteUserInAdmin(id));
        }

        [Route("Admin/ContactUs")]
        public async Task<IActionResult> ContactUs(string filter="",int pageId=1)
        {
            ViewBag.filter = filter;
            ViewBag.pageId = pageId;
            return View(await _userService.GetContactUsInAdmin(filter,pageId));
        }
        [HttpPost]
        [Route("DeleteContactUs")]
        public async Task<IActionResult> DeleteContactUs(long id)
        {
            return Json(await _userService.DeleteContactUs(id)) ;
        }
    }
}
