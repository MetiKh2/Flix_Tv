using Flix_Tv.Domain.Entites.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Serials
{
   public class SerialComment:BaseEntity
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(750, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Text { get; set; }
        public long SerialId { get; set; }
    
        public short? Rate { get; set; }
        public long UserId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        public long? ParentId { get; set; }

        #region Relations
        [ForeignKey("SerialId")]
        public Serial  Serial { get; set; }
        #endregion
    }
}
