using Flix_Tv.Domain.Entites.BaseEntities;
using Flix_Tv.Domain.Entites.Plans;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Users
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage ="ایمیل را به درستی وارد کنید")]
        public string Email { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }
         
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FullName { get; set; }
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Avatar { get; set; }

        public bool IsActive { get; set; }
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ActiveCode { get; set; }


        #region Relations
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public ICollection<UserLoginLog> UserLoginLogs { get; set; }
         public ICollection<UserFavoriteMovie> UserFavoriteMovies { get; set; }
         public ICollection<UserFavoriteSerial> UserFavoriteSerials { get; set; }
        #endregion

    }
}
