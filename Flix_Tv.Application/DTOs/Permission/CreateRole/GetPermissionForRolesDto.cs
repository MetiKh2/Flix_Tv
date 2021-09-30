using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Permission.CreateRole
{
   public class GetPermissionForRolesDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }

    }
}
