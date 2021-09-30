using Flix_Tv.Domain.Entites.BaseEntities;
using Flix_Tv.Domain.Entites.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Permissions
{
    public class Permission : BaseEntity
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Title { get; set; }
        public long? ParentId { get; set; }

        #region Relations
        public ICollection<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
