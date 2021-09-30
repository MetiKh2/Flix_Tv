using Flix_Tv.Domain.Entites.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Users
{
   public class UserLoginLog:BaseEntity
    {
        public long UserId { get; set; }
        public string IP { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        #endregion
    }
}
