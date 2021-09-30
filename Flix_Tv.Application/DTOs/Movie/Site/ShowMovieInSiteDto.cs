using Flix_Tv.Domain.Entites.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Movie.Site
{
  public  class ShowMovieInSiteDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Tiser { get; set; }
        public int Year { get; set; }
        public TimeSpan Time { get; set; }
        public short AgeRestriction { get; set; }
        public string Description { get; set; }
       public List<ShowMovieFileInSiteDto> MovieFiles { get; set; }
        public List<string> Categories { get; set; }
        public string ImageName { get; set; }
        public bool ExistUserFavoriteMovie { get; set; }
        public bool IsFree { get; set; }
          public double? AvvrageRate { get; set; }
        public bool IsActive { get; set; }
    }
    public class ShowMovieFileInSiteDto
    {
        public Quality Quality { get; set; }
        public string FileName { get; set; }
    }
}
