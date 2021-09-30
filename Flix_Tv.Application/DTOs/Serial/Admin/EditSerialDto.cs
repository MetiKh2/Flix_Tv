using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Serial.Admin
{
   public class EditSerialDto
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(120, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Display(Name = "نام سریال")]
        public string Title { get; set; }
        [Display(Name = "محدودیت سنی سریال")]
        public short AgeRestriction { get; set; }
        [MaxLength(3000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Tag { get; set; }
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Director { get; set; }
        public IFormFile Tiser { get; set; }
        public IFormFile Image { get; set; }
        public bool IsFree { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "سال ساخت سریال")]
        public int YearOfCreateDate { get; set; }

        public long Id { get; set; }
        public string LastTiser { get; set; }
        public string LastImage { get; set; }
    }
}
