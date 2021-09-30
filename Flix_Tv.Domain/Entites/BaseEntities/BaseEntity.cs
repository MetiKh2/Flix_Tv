using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.BaseEntities
{
  public  class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CraeteDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime? RemoveDate { get; set; }
    }
}
