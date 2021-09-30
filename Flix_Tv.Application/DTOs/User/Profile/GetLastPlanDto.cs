using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.User.Profile
{
   public class GetLastPlanDto
    {
        public string Title { get; set; }
        public string EndDate { get; set; }
        public long Amount { get; set; }
    }
}
