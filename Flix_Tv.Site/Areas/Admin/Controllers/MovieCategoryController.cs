using Flix_Tv.Application.DTOs.MovieCategory.Admin.Edit;
using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Common.Convertors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieCategoryController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieCategoryController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [Route("Admin/MovieCategories")]
        public async Task<IActionResult >Index(int pageId=1,string filter="")
        {
            ViewBag.pageId = pageId;
            ViewBag.filter = filter;
            return View(await _movieService.GetMovieCategoriesInAdmin(pageId,filter));
        }
        [HttpPost]
        [Route("Admin/CreateMovieCategory")]
        public async Task<IActionResult> CreateMovieCategory(string categoryName, IFormFile categoryImage)
        {
            try
            {
                if (string.IsNullOrEmpty(categoryName) || categoryName.Length > 50)
                {
                    return Json(false);
                }
             var category=  await _movieService.CreateMovieCategory(categoryName,categoryImage);
                
                return Json(category);
            }
            catch
            {
                return Json(false);
                throw;
            }
        }

        [HttpPost]
        [Route("Admin/DeleteMovieCategory")]
        public async Task<IActionResult > DeleteMovieCategory(long Id)
        {
            return Json(await _movieService.DeleteMovieCategory(Id));
        }

        [Route("Admin/EditMovieCategory/{id}")]
        public async Task<IActionResult> EditMovieCategory(long id)
        {
            var category = await _movieService.GetMovieCategoryById(id);
            if (category==null)
            {
                return NotFound();
            }
            return View(new MovieCategoryForEditDto
            {
                Id = category.Id,
                ImageName = category.ImageName,
                Title = category.Title
            });
        }
        [HttpPost]
        [Route("Admin/EditMovieCategory")]
        public async Task<IActionResult> EditMovieCategory(MovieCategoryForEditDto dto,IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
           await _movieService.EditMovieCategory(dto,image);
            return Redirect("/Admin/MovieCategories");
        }
    }
}
