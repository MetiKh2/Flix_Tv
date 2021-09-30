using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.SerialSeason.Admin
{
   public class GetSerialSeasonsInAdminDto
    {
        public string Title { get; set; }
        public long Id { get; set; }
        public string Image { get; set; }
        public string CreateDate { get; set; }

    }
}
