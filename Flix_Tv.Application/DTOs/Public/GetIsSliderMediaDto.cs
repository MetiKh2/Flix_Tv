using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Public
{
   public class GetIsSliderMediaDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string FirstCategory { get; set; }
        public int YearOfCreate { get; set; }
        public bool IsFree { get; set; }
        public string ImageName { get; set; }
        public bool IsSerial { get; set; }
        public double? AvvrageRate { get; set; }
    }
}
