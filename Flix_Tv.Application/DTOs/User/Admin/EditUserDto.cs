using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.User.Admin
{
   public class EditUserDto
    {
        public long Id { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل را به درستی وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور ")]
        [MaxLength(16, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [MinLength(4, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد .")]
        public string Password { get; set; }
        [Display(Name = "نام و نام خانوادگی ")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FullName { get; set; }

        public string LastAvatar { get; set; }
        public string LastEmail { get; set; }
        public string LastUserName { get; set; }

        public IFormFile Avatar { get; set; }
        public bool IsActive { get; set; }
    }
}
