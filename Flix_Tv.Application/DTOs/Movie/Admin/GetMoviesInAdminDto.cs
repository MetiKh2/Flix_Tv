using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Movie.Admin
{
   public class GetMoviesInAdminDto
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TimeSpan Time { get; set; }
        [MaxLength(120, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public short AgeRestriction { get; set; }
        public string Director { get; set; }
        public string ImageSrc { get; set; }
        public bool IsFree { get; set; }
        public long? ViewCount { get; set; }
        public string CreateDate { get; set; }
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsSlider  { get; set; }
        public double? AvvrageRate { get; set; }
        public int YearOfCreateDate { get; set; }
    }
}
