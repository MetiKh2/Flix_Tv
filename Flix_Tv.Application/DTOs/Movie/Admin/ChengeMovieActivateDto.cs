using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Movie.Admin
{
public    class ChengeMovieActivateDto
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
    }
}
