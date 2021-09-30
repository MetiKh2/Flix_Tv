using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Public
{
  public  class GetMediaCategoriesDto
    {
        public long Id { get; set; }
        public int MediaCount { get; set; }
        public bool IsSerial { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
