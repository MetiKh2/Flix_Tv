using Flix_Tv.Domain.Entites.BaseEntities;
using Flix_Tv.Domain.Entites.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Movies
{
    public class Movie : BaseEntity
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TimeSpan Time { get; set; }
        [MaxLength(120, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public short AgeRestriction { get; set; }
        [MaxLength(3000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Tag { get; set; }
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Director { get; set; }
        public string TiserFile { get; set; }
        public string ImageSrc { get; set; }
        public bool IsFree { get; set; }
        public long? ViewCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsSlider { get; set; }
        public int YearOfCreateDate { get; set; }
        #region Relations
        public ICollection<MovieCategoryMovie> MovieCategories { get; set; }
        public ICollection<MovieFile> MovieFiles { get; set; }
        public ICollection<MovieComment> MovieComments { get; set; }
        public ICollection<UserFavoriteMovie> UserFavoriteMovies { get; set; }
        #endregion
    }
}
