using Flix_Tv.Domain.Entites.BaseEntities;
using Flix_Tv.Domain.Entites.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Users
{
  public  class UserRole:BaseEntity
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        #endregion
    }
}
