using Flix_Tv.Domain.Entites.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Serials
{
  public  class SerialCategorySerial:BaseEntity
    {
        public long SerialId { get; set; }
        public long SerialCategoryId { get; set; }


        #region Relations
        [ForeignKey("SerialId")]
        public Serial Serial { get; set; }
        [ForeignKey("SerialCategoryId")]
        public SerialCategory SerialCategory { get; set; }
        #endregion
    }
}
