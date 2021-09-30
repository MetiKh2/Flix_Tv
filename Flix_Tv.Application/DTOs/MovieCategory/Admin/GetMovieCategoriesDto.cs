using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.MovieCategory.Admin
{
  public  class GetMovieCategoriesDto
    {
        public long Id { get; set; }
        public string  DateTime { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
