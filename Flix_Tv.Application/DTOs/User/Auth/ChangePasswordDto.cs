using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.User.Auth
{
  public  class ChangePasswordDto
    {
        [Display(Name = "رمز عبور ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(16, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [MinLength(4, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }
        [Display(Name = " تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(16, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [MinLength(4, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد .")]
        [Compare("Password", ErrorMessage = "رمز عبور با تکرار آن مغایرت دارد")]
        public string RePassword { get; set; }

        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ActiveCode { get; set; }
    }
}
