using Flix_Tv.Domain.Entites.BaseEntities;
using Flix_Tv.Domain.Entites.Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Roles
{
  public  class RolePermission:BaseEntity
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }

        #region Relations
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }
        #endregion
    }
}
