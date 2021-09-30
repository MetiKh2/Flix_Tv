using Flix_Tv.Application.DTOs.AdminPage;
using Flix_Tv.Application.DTOs.Public;
using Flix_Tv.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        private readonly ISerialService _serialService;
        public HomeController(IUserService userService,IMovieService movieService,ISerialService serialService)
        {
            _userService = userService;
            _movieService = movieService;
            _serialService = serialService;
        }

        public async Task<IActionResult >Index()
        {
            var model = new AdminPageDto();
            model.LastUsersInAdminPage =await _userService.GetLastUserInAdminPage(5);
            var lastEpisodeComment =await _serialService.GetLastEpisodeCommentIsnAdminPage(5);
            var lastMovieComment =await _movieService.GetLastMovieCommentInAdminPage(5);
            var lastSerialComment =await _serialService.GetLastSerialCommentIsnAdminPage(5);
            var LastMediaComments = new List<LastCommentInAdminPageDto>();
            LastMediaComments.AddRange(lastEpisodeComment);
            LastMediaComments.AddRange(lastMovieComment);
            LastMediaComments.AddRange(lastSerialComment);
            model.LastCommentsInAdminPage = LastMediaComments.OrderByDescending(p => p.CreateDate).Take(5).ToList();
            var lastMovies = await _movieService.GetLastMovies(5);
            var lastSerials = await _serialService.GetLastSerials(5);
            var lastMedias = new List<GetLastMediasDto>();
            lastMedias.AddRange(lastMovies);
            lastMedias.AddRange(lastSerials);
            model.LastMediasInAdminPage= lastMedias.OrderByDescending(p => p.DateTime).Take(5).ToList();
            var bestMovies =await _movieService.GetBestMoviesInAdminPage(5);
            var bestSerials = await _serialService.GetBestSerialsInAdminPage(5);
            var bestMedias = new List<BestMediaInAdminPageDto>();
            bestMedias.AddRange(bestMovies);
            bestMedias.AddRange(bestSerials);
            model.BestMediasInAdminPage = bestMedias.OrderByDescending(p => p.Rate).Take(5).ToList();
            return View(model);
        }
    }
}
