using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Common.Security
{
   public static class MovieValidator
    {
        public static bool IsMovie(this IFormFile file)
        {
            if (file.FileName.ToLower().EndsWith(".mp4"))
            {
                return true;
            }
            return false;
        }
    }
}
