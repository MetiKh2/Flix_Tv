using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.User.Auth
{
   public class ForgotPasswordEmailDto
    {
        [Display(Name = "نام کاربری")]
        
        public string UserName { get; set; }
         
        [Display(Name = "کد فعالسازی  ")]
    
        public string ActivateCode { get; set; }
    }
}
