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
    public class SerialEpisode : BaseEntity
    {
        public long SerialId { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(120, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        public long SeasonId { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(3000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }

        #region Relations
        [ForeignKey("SerialId")]
        public Serial Serial { get; set; }
        [ForeignKey("SeasonId")]
        public SerialSeason SerialSeason { get; set; }
        public ICollection<SerialEpisodeFile> SerialEpisodeFiles { get; set; }
        public ICollection<SerialEpisodeComment> SerialEpisodeComments { get; set; }

        #endregion
    }
}
