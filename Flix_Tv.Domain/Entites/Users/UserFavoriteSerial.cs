using Flix_Tv.Domain.Entites.BaseEntities;
using Flix_Tv.Domain.Entites.Serials;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Users
{
  public  class UserFavoriteSerial:BaseEntity
    {
        public long UserId { get; set; }
        public long SerialId { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("SerialId")]
        public Serial Serial { get; set; }
        #endregion
    }
}
