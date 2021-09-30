using Flix_Tv.Application.DTOs.Movie;
using Flix_Tv.Application.DTOs.Public;
using Flix_Tv.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site.ViewComponents
{
    public class SliderComponent:ViewComponent
    {
        private readonly IMovieService _movieService;
        private readonly ISerialService _serialService;
        public SliderComponent(IMovieService movieService,ISerialService serialService)
        {
            _serialService = serialService;
            _movieService = movieService;
        }
        public async Task< IViewComponentResult > InvokeAsync()
        {
            var movies = await _movieService.GetIsSliderMovies();
            var serials = await _serialService.GetIsSliderSerials();
            var result = new List<GetIsSliderMediaDto>();
            result.AddRange(movies);
            result.AddRange(serials);
            return View("Slider",result);
        }
    }
}
