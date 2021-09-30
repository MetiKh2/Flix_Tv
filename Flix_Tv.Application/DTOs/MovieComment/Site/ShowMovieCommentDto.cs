using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.MovieComment.Site
{
   public class ShowMovieCommentDto
    {
        public long Id { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public short? Rate { get; set; }
        public string CreateDate { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public string ParentText { get; set; }
        public long? ParentId { get; set; }
    }
}
