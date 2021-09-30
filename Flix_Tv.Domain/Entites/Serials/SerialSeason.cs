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
    public class SerialSeason : BaseEntity
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(120, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        public long SerialId { get; set; }
        public string Image { get; set; }

        #region Relations
        [ForeignKey("SerialId")]
        public Serial Serial { get; set; }
        public ICollection<SerialEpisode> SerialEpisodes { get; set; }
        #endregion
    }
}
