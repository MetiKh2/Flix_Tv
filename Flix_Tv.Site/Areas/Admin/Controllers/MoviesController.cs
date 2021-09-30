using Flix_Tv.Application.DTOs.Movie.Admin;
using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Domain.Entites.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [Route("Admin/Movies")]
        public async Task<IActionResult> Index(int pageId = 1, string sort = "date", string filter = "")
        {
            ViewBag.pageId = pageId;
            ViewBag.filter = filter;
            ViewBag.sort = sort;

            return View(await _movieService.GetMoviesInAdmin(pageId, sort, filter));
        }

        [Route("Admin/CreateMovie")]
        public async Task<IActionResult> CreateMovie()
        {
            ViewBag.MovieCategories = await _movieService.GetMovieCategoriesInAdmin();
            return View();
        }

        [Route("Admin/CreateMovie")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 259715200)]
        public async Task<IActionResult> CreateMovie(CreateMovieDto dto, List<long> selectedMovieCategories)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MovieCategories = await _movieService.GetMovieCategoriesInAdmin();
                return View(dto);
            }
            if (dto.TiserFile != null && dto.TiserFile.FileName.ToLower().EndsWith(".mp4") == false)
            {
                ViewBag.MovieCategories = await _movieService.GetMovieCategoriesInAdmin();
                ModelState.AddModelError("TiserFile", "پسوند تیزر باید mp4 باشد");
                return View(dto);
            }
            var movieId = await _movieService.CreateMovie(dto);
            if (selectedMovieCategories.Any()) await _movieService.CreateMovieCategoryMovies(movieId, selectedMovieCategories);
            return Redirect("/Admin/Movies");
        }

        [HttpPost]
        [Route("Admin/DeleteMovie")]
        public async Task<IActionResult> DeleteMovie(long Id) => Json(await _movieService.DeleteMovie(Id));

        [Route("Admin/EditMovie/{id}")]
        public async Task<IActionResult> EditMovie(long id)
        {
            var movie = await _movieService.GetMovieById(id);
            if (movie == null) return NotFound();
            ViewBag.MovieCategories = await _movieService.GetMovieCategoriesInAdmin();
            ViewBag.MovieCategoriesMovie = await _movieService.GetMovieCategoryIdMoviesIdByMovieId(id);
            return View(new EditMovieDto
            {
                AgeRestriction = movie.AgeRestriction,
                Description = movie.Description,
                IsFree = movie.IsFree,
                LastImage = movie.ImageSrc,
                LastTiser = movie.TiserFile,
                Tag = movie.Tag,
                Title = movie.Title,
                Hours = movie.Time.Hours,
                Minutes = movie.Time.Minutes,
                Seconds = movie.Time.Seconds,
               YearOfCreateDate=movie.YearOfCreateDate,Director=movie.Director
            });
        }
        [Route("Admin/EditMovie/{id}")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 259715200)]
        public async Task<IActionResult> EditMovie(EditMovieDto dto, List<long> selectedMovieCategories)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MovieCategoriesMovie = await _movieService.GetMovieCategoryIdMoviesIdByMovieId(dto.Id);
                ViewBag.MovieCategories = await _movieService.GetMovieCategoriesInAdmin();
                return View(dto);
            }
            if (dto.TiserFile != null && dto.TiserFile.FileName.ToLower().EndsWith(".mp4") == false)
            {
                ViewBag.MovieCategoriesMovie = await _movieService.GetMovieCategoryIdMoviesIdByMovieId(dto.Id);
                ViewBag.MovieCategories = await _movieService.GetMovieCategoriesInAdmin();
                ModelState.AddModelError("TiserFile", "پسوند تیزر باید mp4 باشد");
                return View(dto);
            }
            await _movieService.EditMovie(dto);
            await _movieService.EditMovieCategoryMovies(dto.Id, selectedMovieCategories);
            return Redirect("/Admin/Movies");
        }
        [Route("Admin/ChengeMovieActivate")]
        [HttpPost]
        public async Task<IActionResult> ChengeMovieActivate(long Id)
        {
            var movie = await _movieService.GetMovieById(Id);
            if (movie == null) return NotFound();
            movie.IsActive = !movie.IsActive;
            movie.UpdateDate = DateTime.Now;
            await _movieService.UpdateMovie(movie);
            ChengeMovieActivateDto dto = new ChengeMovieActivateDto() { Id = Id, IsActive = movie.IsActive };
            return Json(dto);
        }
        [Route("Admin/ChengeMovieIsSliderStatus")]
        [HttpPost]
        public async Task<IActionResult> ChengeMovieIsSliderStatus(long Id)
        {
            var movie = await _movieService.GetMovieById(Id);
            if (movie == null) return NotFound();
            movie.IsSlider = !movie.IsSlider;
            movie.UpdateDate = DateTime.Now;
            await _movieService.UpdateMovie(movie);
            ChangeIsSliderMovieStatusDto dto = new ChangeIsSliderMovieStatusDto() { Id = Id, IsSlider = movie.IsSlider };
            return Json(dto);
        }
        [Route("Admin/CreateMovieFile/{movieId}")]
        public async Task<IActionResult> CreateMovieFile(long movieId)
        {
            if (!await _movieService.MovieExists(movieId)) return NotFound();
            ViewBag.MovieTitle = await _movieService.GetMovieTitleById(movieId);
            ViewBag.MovieId = movieId;
            return View();
        }
        [Route("Admin/CreateMovieFile")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 4009715200)]
        public async Task<IActionResult> CreateMovieFile(long movieId, IFormFile movieFile, Quality? quality)
        {
            if (!await _movieService.MovieExists(movieId)) return NotFound();
            ViewBag.MovieTitle = await _movieService.GetMovieTitleById(movieId);
            ViewBag.MovieId = movieId;
            if (movieFile == null)
            {
                ViewBag.error = true;
                return View();
            }
            if (quality==null)
            {
                ViewBag.error = true;
                return View();
            }
            if (movieFile.FileName.ToLower().EndsWith(".mp4") == false)
            {  
                ViewBag.errorExtension = true;
                return View();
            }
           var pathName= await  _movieService.CreateMovieFile(movieId,movieFile,quality.Value);
            ViewBag.Success = true;
            ViewBag.moviePath = pathName.Substring(39).Replace(@"\","/");
            return View();
        }
        [Route("Admin/getMovieFile/{movieId}")]
        public async Task<IActionResult> getMovieFile(long movieId,Quality quality)
        {
            var movieSrc = await _movieService.GetMovieFileSrcByMovieIdAndQuality(movieId,quality);
            if (string.IsNullOrWhiteSpace(movieSrc)) return NotFound();
            string qualityString = quality.ToString();
            if (quality != Quality.Q4k) qualityString+="p";
            ViewBag.fileName = movieSrc;
             ViewBag.quality = qualityString.Substring(1).ToLower();
             return View();
        }
        //public async Task<IActionResult> getMovieFile(long movieId, Quality quality)
        //{
        //    var movieSrc = await _movieService.GetMovieFileSrcByMovieIdAndQuality(movieId, quality);
        //    if (string.IsNullOrWhiteSpace(movieSrc)) return NotFound();
        //    string qualityString = quality.ToString();
        //    if (quality != Quality.Q4k) qualityString += "p";
        //    //return Redirect("/Movies/Files/"+ qualityString.Substring(1) +"/"+movieSrc);
        //    ViewBag.fileName = movieSrc;
        //    ViewBag.quality = quality;
        //    return View();
        //}

        [Route("Admin/movieComments/{movieId}")]
        public async Task<IActionResult> MovieComments(long movieId,int pageId=1,string filter="")
        {
            if (!await _movieService.MovieExists(movieId)) return NotFound();
            ViewBag.pageId = pageId;
            ViewBag.filter = filter;
            ViewBag.movieId = movieId;
            return View(await _movieService.ShowMovieCommentsInAdmin(movieId,pageId,filter));
        }
        [Route("Admin/replymovieComments/{parentId}")]
        public async Task<IActionResult> ReplyMovieComments(long? parentId)
        {
            if (parentId == null) return NotFound();
            if (!await _movieService.ExistComment(parentId.Value)) return NotFound();
            return View(await _movieService.ShowSubMovieCommentsInAdmin(parentId));
        }
        [Route("DeleteMovieComment")]
        [HttpPost]
    public async Task<IActionResult> DeleteMovieComment(long id)
        {
            return Json(await _movieService.DeleteMovieComment(id));
        }
    }
}
