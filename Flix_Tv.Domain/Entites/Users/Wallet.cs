using Flix_Tv.Domain.Entites.BaseEntities;
using Flix_Tv.Domain.Entites.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Users
{
    public class Wallet : BaseEntity
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long Amount { get; set; }

        public WalletType WalletType { get; set; }

        public long UserId { get; set; }

        public bool IsPay { get; set; }
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }


        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        #endregion
    }

    

   
}
