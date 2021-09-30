using Flix_Tv.Domain.Entites.BaseEntities;
using Flix_Tv.Domain.Entites.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Serials
{
    public class Serial : BaseEntity
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(120, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        [MaxLength(120, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public short AgeRestriction { get; set; }
        [MaxLength(3000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Tag { get; set; }
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Director { get; set; }
        public string Tiser { get; set; }
        public string Image { get; set; }
        public bool IsFree { get; set; }

        public long? ViewCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsSlider { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int YearOfCreateDate { get; set; }

        #region Relations
        public ICollection<SerialCategorySerial> SerialCategorySerials { get; set; }
        public ICollection<SerialSeason> SerialSeasons { get; set; }
        public ICollection<SerialEpisode> SerialEpisodes { get; set; }
        public ICollection<UserFavoriteSerial> UserFavoriteSerials { get; set; }
        public ICollection<SerialComment> SerialComments { get; set; }
        #endregion
    }
}
