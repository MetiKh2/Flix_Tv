using Flix_Tv.Application.DTOs.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.AdminPage
{
   public class AdminPageDto
    {
        public List<LastUserInAdminPageDto>  LastUsersInAdminPage { get; set; }
        public List<LastCommentInAdminPageDto> LastCommentsInAdminPage  { get; set; }
        public List<GetLastMediasDto>  LastMediasInAdminPage { get; set; }
        public List<BestMediaInAdminPageDto> BestMediasInAdminPage { get; set; }
    }
    public class LastUserInAdminPageDto
    {
        public string FullName { get; set; }
        public string Email{ get; set; }
        public string UserName{ get; set; }
    }
    public class LastCommentInAdminPageDto
    {
        public DateTime CreateDate { get; set; }
        public string Subject { get; set; }
        public short? Rate{ get; set; }
        public string UserName { get; set; }
        public string MediaName { get; set; }
    }
  
    public class BestMediaInAdminPageDto
    {
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public bool IsSerial { get; set; }
        public double? Rate { get; set; }

    }
}
