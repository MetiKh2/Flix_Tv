using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.User.Profile
{
public    class GetLastUserCommentsDto
    {
        public long Id  { get; set; }
        public string Subject { get; set; }
        public short? rate  { get; set; }
        public string MediaTitle { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
