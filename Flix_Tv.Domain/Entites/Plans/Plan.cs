using Flix_Tv.Domain.Entites.BaseEntities;
using Flix_Tv.Domain.Entites.Enums;
using Flix_Tv.Domain.Entites.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Plans
{
  public  class Plan:BaseEntity
    {
        public long UserId { get; set; }

        public PlanType PlanType { get; set; }
        public DateTime PlanEnd { get; set; }

        public long PlanAmount { get; set; }

        #region Relations   
        [ForeignKey("UserId")]
        public User User { get; set; }
        #endregion
    }

   
}
