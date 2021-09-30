using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.User.Profile
{
   public class GetFavoriteMediasDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsFree { get; set; }
        public bool IsSerial { get; set; }
        public int Year { get; set; }
        public string FirstCategory { get; set; }
        public string Image { get; set; }
    }
}
