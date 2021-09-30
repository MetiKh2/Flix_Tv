using Flix_Tv.Application.DTOs.User.Profile;
using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Common.Generators;
using Flix_Tv.Common.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site.Areas.Profile.Controllers
{
    [Area("Profile")]
    [Authorize]
    public class UserPanelController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        private readonly ISerialService _serialService;
        public UserPanelController(IUserService userService,ISerialService serialService,IMovieService movieService)
        {
            _serialService = serialService;
            _movieService = movieService;
            _userService = userService;
        }
        [Route("Profile")]

        public async Task<IActionResult> Index(string successPay,string addplan)
        {
            ViewBag.addplan = addplan;
            ViewBag.successPay = successPay;
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            if (user == null)
                return NotFound();
            
            
            var favoriteMovies =await _movieService.GetFavoriteMovies(User.GetUserId());
            var favoriteSerials =await _serialService.GetFavoriteSerials(User.GetUserId());
            var favoriteMedias = new List<GetFavoriteMediasDto>();
            favoriteMedias.AddRange(favoriteMovies);
            favoriteMedias.AddRange(favoriteSerials);

            var lastMovieComments = await _movieService.GetLastUserMovieComments(User.GetUserId(),5);
            var lastSerialComments = await _serialService.GetLastUserSerialComments(User.GetUserId(),5);
            var lastEpisodeComments = await _serialService.GetLastUserEpisodeComments(User.GetUserId(),5);
            var lastComments = new List<GetLastUserCommentsDto>();
            lastComments.AddRange(lastEpisodeComments);
            lastComments.AddRange(lastSerialComments);
            lastComments.AddRange(lastMovieComments);

            var model = new UserProfileDto
            {
                Email = user.Email,
                FullName = user.FullName,
                WalletRemain = await _userService.GetSumWallets(user.Id),
                CommentCount = await _userService.GetUserCommentCount(user.Id)
                , RateCount = await _userService.GetUserRateCount(user.Id),
                FavoriteMedias = favoriteMedias,
                LastUserComments =lastComments.OrderByDescending(p=>p.CreateDate).Take(5).ToList() ,
                LastPlan=await _userService.GetLastPlan(User.GetUserId())
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeFullName(string fullName)
        {
            return Json(await _userService.ChangeFullNameInProfile(User.Identity.Name, fullName));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string reNewPassword)
        {
            if (newPassword.Length > 16 || newPassword.Length < 4)
            {
                return Json(false);
            }
            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(reNewPassword))
            {
                return Json(false);
            }
            if (newPassword != reNewPassword)
            {
                return Json(false);
            }
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            if (user.Password != MyPasswordHasher.EncodePasswordMd5(currentPassword))
            {
                return Json("WrongCurrentPassword");
            }
            user.Password = MyPasswordHasher.EncodePasswordMd5(newPassword);
            await _userService.UpdateUser(user);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Json(true);
        }
        [Authorize]
        public async Task<IActionResult> CreateWallet(string amount)
        {
            if (string.IsNullOrWhiteSpace(amount))
                return NotFound();
            try
            {
                long testAmountResult = Convert.ToInt64(amount);
            }
            catch (Exception)
            {
                return NotFound();
                throw;
            }
            long userId = await _userService.GetUserIdByUserName(User.Identity.Name);
            long amountResult = Convert.ToInt64(amount);
            long walletId = await _userService.CreateWallet(amountResult, userId,Domain.Entites.Enums.WalletType.Deposit,false);

            #region Online payment

            var payment = new ZarinpalSandbox.Payment((int)amountResult);

            var res = payment.PaymentRequest("شارژ کیف پول", "https://localhost:44308/OnlinePayment/" + walletId);

            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            #endregion

            return null;
        }
        [Route("OnlinePayment/{id}")]
        public async Task<IActionResult> OnlinePayment(long id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                 HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                 && HttpContext.Request.Query["Authority"] != "")
            {

                var wallet = await _userService.GetWalletById(id);

                string authority = HttpContext.Request.Query["Authority"];

                var payment = new ZarinpalSandbox.Payment((int)wallet.Amount);

                var res = payment.Verification(authority).Result;

                if (res.Status == 100)
                {

                    wallet.IsPay = true;
                   await _userService.UpdateWallet(wallet);
                    return Redirect("/profile?successPay=true");
                }
            }
            return Redirect("/profile?successPay=false");
        }

    }
}
