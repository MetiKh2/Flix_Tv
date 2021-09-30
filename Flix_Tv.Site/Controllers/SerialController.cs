using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Common.Generators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site.Controllers
{
    public class SerialController : Controller
    {
        private readonly ISerialService _serialService;
        private readonly IUserService _userService;
        public SerialController(ISerialService serialService,IUserService userService)
        {
            _userService = userService;
            _serialService = serialService;
        }
        [Route("Serials")]
        public async Task<IActionResult> Index(int pageId = 1, string filter = "", long categoryId = 0, long year = 0, string sort = "date")
        {
            if (await _serialService.ExistsSerialCategory(categoryId))
            {
                ViewBag.category = await _serialService.GetSerialCategoryTitleById(categoryId);
                ViewBag.categoryId = categoryId;
            }
            else ViewBag.category = "0";
            ViewBag.filterSerial = filter;
            ViewBag.year = year;
            ViewBag.sort = sort;
            ViewBag.pageId = pageId;
            ViewBag.serialCateggorySerial = await _serialService.GetSerialCategories();
            return View(await _serialService.GetSerialsInSite(pageId, filter, categoryId, year, sort));
        }

        [Route("Serial/{id}")]
        public async Task<IActionResult> ShowSerial(long id)
        {
            if (!await _serialService.ExistsSerial(id)) return NotFound();
            ViewBag.serialId = id;
            var model = await _serialService.ShowSerialInSite(id);
            if (!model.IsActive) return NotFound();
          if(User.Identity.IsAuthenticated)  model.ExistUserFavoriteSerial = await _serialService.ExistsUserFavoriteSerial(User.GetUserId(),id);
            ViewBag.maybeLikeSerials = await _serialService.GetMaybeLikeSerials(id);
            await _serialService.IncreaseSerialViewCount(id);
            return View(model) ;
        }
        [Route("Episode/{id}")]
        public async Task<IActionResult> ShowEpisode( long id)
        {
            if (!await _serialService.ExistsEpisode(id)) return NotFound();
            var model = await _serialService.ShowSerialEpisode(id);
            if (await _serialService.GetIsActiveSerial(model.SerialId) == false) return NotFound(); 
            if (!await _serialService.ExistsSerial(model.SerialId)||await _serialService.GetSerialIdByEpisodeId(id)!=model.SerialId) return NotFound();
            if (!await _serialService.IsFreeSerial(model.SerialId))
            {
                if (!User.Identity.IsAuthenticated) ViewBag.HavePlan = false;
                else ViewBag.HavePlan = await _userService.ExistUserPlan(User.GetUserId());
            }
            else
            {
                ViewBag.HavePlan = true;
            }
            ViewBag.NearEpisodes = await _serialService.ShowNearEpisode(id,model.SerialId);
            ViewBag.maybeLikeSerials = await _serialService.GetMaybeLikeSerials(model.SerialId);
            return View(model);
        }

        [HttpPost]
        [Route("CreateSerialComment")]
        public async Task<IActionResult> CreateSerialComment(string text, string subject, short? rate, long serialId, long? parentId)
        {
            if (!await _serialService.ExistsSerial(serialId)) return NotFound();
            if (!User.Identity.IsAuthenticated) return Json("NoAuth");
            if (rate != null && await _serialService.ExistsUserSerialVote( serialId,User.Identity.Name)) rate = null;
            if (rate > 10 || rate <= 0) rate = null;
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(subject))return NotFound();
            if (text.Length > 750 || subject.Length > 100) return NotFound();
            if (parentId != null) rate = null;
            return Json(await _serialService.CreateSerialComment(text, subject, rate, User.Identity.Name, serialId, parentId));
        }
        [Route("ShowSerialComments/{serialId}")]
        public async Task<IActionResult> ShowSerialComments(long serialId, int pageId=1)
        {
            if (!await _serialService.ExistsSerial(serialId)) return null;
            ViewBag.pageId = pageId;
            ViewBag.subComments = await _serialService.ShowSubSerialComments(serialId);
            return View(await _serialService.ShowSerialComment(serialId, pageId));
             
        }

        [HttpPost]
        [Route("CreateEpisodeComment")]
        public async Task<IActionResult> CreateEpisodeComment(string text, string subject, short? rate, long episodeId, long? parentId)
        {
            if (!await _serialService.ExistsEpisode(episodeId)) return NotFound();
            if (!User.Identity.IsAuthenticated) return Json("NoAuth");
            if (rate != null && await _serialService.ExistsUserEpisodeVote(episodeId, User.GetUserId())) rate = null;
            if (rate > 10 || rate <= 0) rate = null;
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(subject)) return NotFound();
            if (text.Length > 750 || subject.Length > 100) return NotFound();
            if (parentId != null) rate = null;
            return Json(await _serialService.CreateEpisodeComment(text, subject, rate, User.GetUserId(), episodeId, parentId));
        }

        [Route("ShowEpisodeComments/{episodeId}")]
        public async Task<IActionResult> ShowEpisodeComments(long episodeId, int pageId = 1)
        {
            if (!await _serialService.ExistsEpisode(episodeId)) return null;
            ViewBag.pageId = pageId;
            ViewBag.subComments = await _serialService.ShowSubEpisodeComments(episodeId);
            return View(await _serialService.ShowEpisodeComment(episodeId, pageId));

        }
        [HttpPost]
        [Route("AddUserFavoriteSerial")]
        public async Task<IActionResult> AddUserFavoriteSerial(long serialId)
        {
            if (!await _serialService.ExistsUserFavoriteSerial(User.GetUserId(), serialId))
            {
                await _serialService.CreateUserFavoriteSerial(User.GetUserId(), serialId);
                return Json(true);
            }
            else
            {
                await _serialService.DeleteUserFavoriteSerial(User.GetUserId(), serialId);
                return Json(false);
            }

        }
        [Route("CheckEpisodeFile")]
        public async Task<IActionResult> CheckEpisodeFile(string q, string f)
        {
            var episodeId = await _serialService.GetEpisodeIdByFileName(f);
            if (!await _serialService.ExistsEpisode(episodeId)) return NotFound();
            var episode = await _serialService.GetEpisodeById(episodeId);
            if (!await _serialService.ExistsSerial(episode.SerialId)) return NotFound();
            var serial = await _serialService.GetSerialById(episode.SerialId);
            if (!serial.IsFree)
            {
                if (!User.Identity.IsAuthenticated) return NotFound();
                if (!await _userService.ExistUserPlan(User.GetUserId())) return NotFound();
            }
            ViewBag.fileName = f;
            ViewBag.quality = q;
            return View();
        }
        [Route("dEpisodeFile")]
        public async Task<IActionResult> dEpisodeFile(string q, string f)
        {
            var episodeId = await _serialService.GetEpisodeIdByFileName(f);
            if (!await _serialService.ExistsEpisode(episodeId)) return NotFound();
            var episode = await _serialService.GetEpisodeById(episodeId);
            if (!await _serialService.ExistsSerial(episode.SerialId)) return NotFound();
            var serial = await _serialService.GetSerialById(episode.SerialId);
            if (!serial.IsFree)
            {
                if (!User.Identity.IsAuthenticated) return NotFound();
                if (!await _userService.ExistUserPlan(User.GetUserId())) return NotFound();
            }
            byte[] file = System.IO.File.ReadAllBytes($"wwwroot/Serials/Files/{q}/{f}");
            return File(file, "application/force-download", f);
        }
        //[Route("dMovieFile")]
        //public async Task<IActionResult> dEpisodeFile(string q, string f)
        //{
        //    if (!await _movieService.MovieExists(await _movieService.GetMovieIdByFileName(f))) return NotFound();
        //    var movie = await _movieService.GetMovieById(await _movieService.GetMovieIdByFileName(f));
        //    if (!movie.IsFree)
        //    {
        //        if (!User.Identity.IsAuthenticated) return NotFound();
        //        if (!await _userService.ExistUserPlan(User.GetUserId())) return NotFound();
        //    }

        //    byte[] file = System.IO.File.ReadAllBytes($"wwwroot/Movies/Files/{q}/{f}");
        //    return File(file, "application/force-download", f);
        //}
    }
}
