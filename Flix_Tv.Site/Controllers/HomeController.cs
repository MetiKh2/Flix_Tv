using Flix_Tv.Application.DTOs.Public;
using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Common.Generators;
using Flix_Tv.Domain.Entites.Enums;
using Flix_Tv.Domain.Entites.SettingSite;
using Flix_Tv.Site.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        private readonly ISerialService _serialService;
        private readonly IUserService _userService;
        public HomeController(IMovieService movieService,ISerialService serialService,IUserService userService)
        {
            _userService = userService;
            _movieService = movieService;
            _serialService = serialService;
        }

        public async Task<IActionResult> Index(bool planExist)
        {
            ViewBag.planExist = planExist;
            var movies =await _movieService.GetLastMovies(6);
            var serials =await _serialService.GetLastSerials(6);
            var lastMedias = new List<GetLastMediasDto>();
            lastMedias.AddRange(serials);
            lastMedias.AddRange(movies);
            ViewBag.LastMedias = lastMedias.OrderByDescending(p=>p.DateTime).ToList();
            return View();
        }

        [Route("/Genres")]
        public async Task<IActionResult> Genres()
        {
            var serialCategories =await _serialService.GetSerialCategoriesForSite();
            var movieCategories =await _movieService.GetMovieCategoriesForSite();
            var result = new List<GetMediaCategoriesDto>();
            result.AddRange(serialCategories);
            result.AddRange(movieCategories);
            return View(result);
        }
        [HttpPost]
        [Route("AddPlan")]
        public async Task<IActionResult> AddPlan(PlanType planType)
        {
            if (User.Identity.IsAuthenticated == false) return Redirect("/Login");
            if (planType == null) return NotFound();
            if (await _userService.ExistUserPlan(User.GetUserId()))
            {
                return Redirect("/?planExist=true#plans");
            }
            else
            {
                long amount=_userService.GetAmountByPlanType(planType);
                
                if (amount == 0) return NotFound();
                if (await _userService.GetSumWallets(User.GetUserId())>=(int)amount)
                {
                    await _userService.CreateWallet(amount,User.GetUserId(),WalletType.pay,true);
                    await _userService.CreatePlan(User.GetUserId(), planType);
                    return Redirect("/profile?addplan=true");
                }
                else
                {
                    #region Online payment

                    var payment = new ZarinpalSandbox.Payment((int)amount);

                    var res = payment.PaymentRequest("خرید شتراک", "https://localhost:44308/OnlinePlanPayment/" + planType);

                    if (res.Result.Status == 100)
                    {
                        return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
                    }
                    #endregion
                }

                return NotFound();
            }
          
        }
        [Route("OnlinePlanPayment/{planType}")]
        public async Task<IActionResult> OnlinePayment(PlanType planType)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                 HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                 && HttpContext.Request.Query["Authority"] != "")
            {

                
          
                string authority = HttpContext.Request.Query["Authority"];

                var payment = new ZarinpalSandbox.Payment((int)_userService.GetAmountByPlanType(planType));

                var res = payment.Verification(authority).Result;

                if (res.Status == 100)
                {

                    await _userService.CreatePlan(User.GetUserId(), planType);
                    return Redirect("/profile?addplan=true");
                }
            }
            return Redirect("/profile?addplan=false");
        }
        public IActionResult Privacy()
        {
            return View();
        }

       // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
       [Route("Error")]
        public IActionResult Error()
        {
            // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return View();
        }

        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }
        [Route("ContactUs")]
        [HttpPost]
        public async Task <IActionResult >ContactUs(ContactUs contactUs)
        {
            if (!ModelState.IsValid)
            {
                return View(contactUs);
            }
            contactUs.CraeteDate = DateTime.Now;
           await _userService.CreateContactUs(contactUs);
            ViewBag.isSuccess = true;
            return View();
        }


    //    [HttpPost]
    //    [Route("file-upload")]
    //    public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
    //    {
    //        if (upload.Length <= 0) return null;

    //        var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



    //        var path = Path.Combine(
    //            Directory.GetCurrentDirectory(), "wwwroot/CommentsFile",
    //            fileName);

    //        using (var stream = new FileStream(path, FileMode.Create))
    //        {
    //            upload.CopyTo(stream);

    //        }



    //        var url = $"{"/CommentsFile/"}{fileName}";


    //        return Json(new { uploaded = true, url });
    //    }
    }
}
