using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Movie.Admin
{
   public class CreateMovieDto
    {
        [Display(Name ="نام فیلم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        [Display(Name = "ساعت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Hours { get; set; }
        [Display(Name = " دقیقه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Minutes { get; set; }
        [Display(Name = " ثانیه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Seconds { get; set; }
        [Display(Name = "محدودیت سنی ")]
     //   [MaxLength(120, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public short AgeRestriction { get; set; }
        [Display(Name = " شرح ")]
        [MaxLength(3000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }
        [Display(Name = " برچسب ها ")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Tag { get; set; }
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Director { get; set; }
        public IFormFile TiserFile { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool IsFree { get; set; }
        public int YearOfCreateDate { get; set; }
    }
}
