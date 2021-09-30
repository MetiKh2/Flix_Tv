using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Serial.Site
{
   public class GetSerialsInSiteDto
    {
        public long Id { get; set; }
        public bool IsFree { get; set; }
        public string FirstCategory { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public DateTime DateTime { get; set; }
        public double? AvvrageRate { get; set; }
    }
}
