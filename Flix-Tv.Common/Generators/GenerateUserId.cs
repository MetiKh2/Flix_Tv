using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Common.Generators
{
   public static class GenerateUserId
    {
        public static long GetUserId(this ClaimsPrincipal? claims)
        {
            var userId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Convert.ToInt64(userId);
        }

    }
}
