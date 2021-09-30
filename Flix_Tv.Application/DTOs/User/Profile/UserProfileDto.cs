using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.User.Profile
{
  public  class UserProfileDto
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل را به درستی وارد کنید")]
        public string Email { get; set; }

        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FullName { get; set; }

        public long WalletRemain { get; set; }
        public int CommentCount { get; set; }
        public int RateCount { get; set; }
        public List<GetFavoriteMediasDto>  FavoriteMedias { get; set; }
        public List<GetLastUserCommentsDto> LastUserComments { get; set; }
        public GetLastPlanDto LastPlan { get; set; }
    }
}
