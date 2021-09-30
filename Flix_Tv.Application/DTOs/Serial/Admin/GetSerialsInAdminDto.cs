using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Serial.Admin
{
    public class GetSerialsInAdminDto
    {

        public string Title { get; set; }
        public long Id { get; set; }
        public short AgeRestriction { get; set; }
        public string Director { get; set; }
        public string Image { get; set; }
        public bool IsFree { get; set; }

        public long? ViewCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsSlider { get; set; }
        public string CreateDate { get; set; }
        public double? AvvrageRate { get; set; }
        public int YearOfCreateDate { get; set; }
    }
}
