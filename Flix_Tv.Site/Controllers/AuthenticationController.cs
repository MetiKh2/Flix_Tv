using Flix_Tv.Application.DTOs.User.Auth;
using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Common.Convertors;
using Flix_Tv.Common.Generators;
using Flix_Tv.Common.Senders;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Flix_Tv.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _viewRenderService;
        public AuthenticationController(IUserService userService, IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
            _userService = userService;
        }
        #region Login

        [Route("Login")]
        public IActionResult Login(bool ActiveSuccess,bool changed)
        {
            ViewBag.changed = changed;
            ViewBag.ActiveSuccess = ActiveSuccess;
            return View();
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var user = await _userService.UserLogin(login);
            if (user == null)
            {
                ModelState.AddModelError("Password", "حسابی با مشخصات وارد شده یافت نشد");
                return View(login);
            }
            var Claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Email,user.Email),
            };

            var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe,
            };

            await HttpContext.SignInAsync(principal, properties);
           await _userService.CreateUserLoginLog(user.Id,Request.HttpContext.Connection.RemoteIpAddress.ToString());
            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion

        #region Register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            if (await _userService.IsEmailExist(register.Email))
            {
                ModelState.AddModelError("Email", "ایمیل وارد شده قبلا توسط شخص دیگری وارد شده");
                return View(register);
            }
            if (await _userService.IsUserNameExist(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری وارد شده قبلا توسط شخص دیگری وارد شده");
                return View(register);
            }
            if (!register.AllowRulesSite)
            {
                ModelState.AddModelError("AllowRulesSite", "لطفا قوانین سایت را مطالعه و تایید کنید");
                return View(register);
            }

            register.ActiveCode = GeneratCode.GenerateUniqCode();
            await _userService.RegisterUser(register);

            string EmailBody = _viewRenderService.RenderToStringAsync("_ActivateEmail", register);
            EmailSender.Send(FixText.FixEmail(register.Email), "فعالسازی حساب کاربری", EmailBody);
            ViewBag.IsSeccess = true;
            return View();
        }



        #endregion


        #region ActivateAccount
        [Route("ActivateAccount/{id}")]
        public async Task<IActionResult> ActivateAccount(string id)
        {
            if (!await _userService.IsActivateCodeExists(id))
            {
                return NotFound();
            }
            await _userService.ActiveUser(id);
            return Redirect("/Login?ActiveSuccess=true");
        }
        #endregion


        #region ForgotPassword
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgot)
        {
            if (!ModelState.IsValid)
            {
                return View(forgot);
            }
            if (!await _userService.IsEmailExist(forgot.Email))
            {
                ModelState.AddModelError("Email","شخصی با ایمیل وارد شده در سایت ثبت نام نکرده است");
                return View(forgot);
            }
            var user =await _userService.GetUserByEmail(forgot.Email);
            var forgotPasswordEmailDto = new ForgotPasswordEmailDto { 
            ActivateCode=user.ActiveCode,
            UserName=user.UserName
            };
            string EmailBody = _viewRenderService.RenderToStringAsync("_ForgotPasswordEmail", forgotPasswordEmailDto);
            EmailSender.Send(FixText.FixEmail(forgot.Email), "  فراموشی رمز عبور", EmailBody);
            ViewBag.IsSuccess = true;
            return View();    
        }

        [Route("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(string id)
        {
            if (!await _userService.IsActivateCodeExists(id))
            {
                return NotFound();
            }
            var changePasswordDto = new ChangePasswordDto
            {
                ActiveCode = id
            };
            return View(changePasswordDto);
        }
        [HttpPost]
        [Route("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto change)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
           await _userService.ChangeForgotPassword(change);
            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion
        #region LogOut
        [Route("LogOut")]
        public async Task<IActionResult>LogOut()
        {
        await    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion
    }
}
