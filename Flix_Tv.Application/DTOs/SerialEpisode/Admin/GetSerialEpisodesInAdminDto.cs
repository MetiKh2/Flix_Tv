using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.SerialEpisode.Admin
{
   public class GetSerialEpisodesInAdminDto
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(120, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        public long Id { get; set; }
        public string Image { get; set; }
        public string CreateDate { get; set; }

    }
}
