using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Common.Generators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        public MovieController(IMovieService movieService,IUserService userService)
        {
            _userService = userService;
            _movieService = movieService;
        }
        [Route("Movies")]
        public async Task<IActionResult> Index(int pageId = 1, string filter = "", long categoryId = 0, long year = 0, string sort = "date")
        {
            if (await _movieService.ExistsMovieCategory(categoryId))
            {
                ViewBag.category = await _movieService.GetMovieCategoryTitleById(categoryId);
                ViewBag.categoryId = categoryId;
            }
            else ViewBag.category = "0";
            ViewBag.filterMovie = filter;
            ViewBag.year = year;
            ViewBag.sort = sort;
            ViewBag.pageId = pageId;
            ViewBag.movieCategories = await _movieService.GetMovieCategoriesInAdmin();
            return View(await _movieService.GetMoviesInSite(pageId, filter, categoryId, year, sort));
        }
        [Route("Movie/{id}")]
        public async Task<IActionResult> ShowMovie(long id)
        {
            if (!await _movieService.MovieExists(id)) return NotFound();
            var model = await _movieService.ShowMovieInSite(id);
            if (!model.IsActive) return NotFound();
            if(User.Identity.IsAuthenticated) model.ExistUserFavoriteMovie = await _movieService.ExistsUserFavoriteMovie(User.GetUserId(), id);
            if (!model.IsFree)
            {
                if (!User.Identity.IsAuthenticated) ViewBag.HavePlan = false;
                else ViewBag.HavePlan =await _userService.ExistUserPlan(User.GetUserId());
            }
            else
            {
                ViewBag.HavePlan = true;
            }
            ViewBag.maybeLikeMovies = await _movieService.GetMaybeLikeMovies(id);
            await _movieService.IncreaseMovieViewCount(id);
            return View(model);
        }
        [Route("ShowMovieComments/{movieId}")]
        public async Task<IActionResult> ShowComments(long movieId, int pageId = 1)
        {
            if (!await _movieService.MovieExists(movieId)) return null;
            ViewBag.pageId = pageId;
            ViewBag.subComments = await _movieService.ShowSubMovieComments(movieId);
            return View(await _movieService.ShowMovieComment(movieId, pageId));
        }

        [HttpPost]
        [Route("CreateMovieComment")]
        public async Task<IActionResult> CreateMovieComment(string text, string subject, short? rate, long movieId, long? parentId)
        {
            if (!await _movieService.MovieExists(movieId)) return NotFound();
            if (!User.Identity.IsAuthenticated) return Json("NoAuth");
            if (rate != null && await _movieService.ExistsUserMovieVote(User.Identity.Name, movieId)) rate = null;
            if (rate > 10 || rate <= 0) rate = null;
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(subject))
                return NotFound();
            if (text.Length > 750 || subject.Length > 100) return NotFound();
            if (parentId != null) rate = null;
            return Json(await _movieService.CreateMovieComment(text, subject, rate, User.Identity.Name, movieId, parentId));
        }

        [HttpPost]
        [Route("AddUserFavoriteMovie")]
        public async Task<IActionResult> AddUserFavoriteMovie(long movieId)
        {
            if (!await _movieService.ExistsUserFavoriteMovie(User.GetUserId(), movieId))
            {
                await _movieService.CreateUserFavoriteMovie(User.GetUserId(), movieId);
                return Json(true);
            }
            else
            {
                await _movieService.DeleteUserFavoriteMovie(User.GetUserId(), movieId);
                return Json(false);
            }

        }

        [Route("CheckMovieFile")]
        public async Task<IActionResult> CheckMovieFile(string q,string f)
        {

            var movieId = await _movieService.GetMovieIdByFileName(f);
            if (!await _movieService.MovieExists(movieId)) return NotFound();
            var movie = await _movieService.GetMovieById(movieId);
            if (!movie.IsFree)
            {
                if (!User.Identity.IsAuthenticated) return NotFound();
                if (!await _userService.ExistUserPlan(User.GetUserId())) return NotFound();
            }
            ViewBag.fileName = f;
            ViewBag.quality = q;
            return View();
        }
        [Route("dMovieFile")]
        public async Task< IActionResult >dMovieFile(string q, string f)
        {
            var movieId = await _movieService.GetMovieIdByFileName(f);
            if (!await _movieService.MovieExists(movieId)) return NotFound();
            var movie = await _movieService.GetMovieById(movieId);
            if (!movie.IsFree)
            {
                if (!User.Identity.IsAuthenticated) return NotFound();
                if (!await _userService.ExistUserPlan(User.GetUserId())) return NotFound();
            }
           
            byte[] file = System.IO.File.ReadAllBytes($"wwwroot/Movies/Files/{q}/{f}");
            return File(file, "application/force-download",f);
        }
    }
}
