using Flix_Tv.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private  IPermissionService _permissionService;
        private long _permissionId = 0;
        public PermissionCheckerAttribute(long permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
             _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                if ( !_permissionService.CheckPermission(context.HttpContext.User.Identity.Name,_permissionId))
                {
                    context.HttpContext.Response.StatusCode = 404;
                   
                    //context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    //context.HttpContext.Response.Redirect("/LogOut");
                    context.HttpContext.Response.Redirect("/Login");
                }
                
            }
            else
            {
                 context.HttpContext.Response.Redirect("/Login");
            }
        }
    }
}
