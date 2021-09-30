using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Roles.Admin
{
   public class GetRolesDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string CreateDate { get; set; }
    }
}
