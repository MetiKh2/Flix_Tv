using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.User.Admin
{
   public class UsersListDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public string CreateDate { get; set; }
        public string Avatar { get; set; }

    }
}
