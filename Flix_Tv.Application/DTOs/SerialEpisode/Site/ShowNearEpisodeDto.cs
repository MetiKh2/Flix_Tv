using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.SerialEpisode.Site
{
   public class ShowNearEpisodeDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public double? AvvrageRate { get; set; }
    }
}
