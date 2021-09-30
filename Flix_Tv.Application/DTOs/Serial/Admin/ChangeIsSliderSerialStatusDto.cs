using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Serial.Admin
{
  public  class ChangeIsSliderSerialStatusDto
    {
        public long Id { get; set; }
        public bool IsSlider { get; set; }
    }
}
