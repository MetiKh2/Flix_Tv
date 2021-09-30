using Flix_Tv.Domain.Entites.BaseEntities;
using Flix_Tv.Domain.Entites.Movies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Users
{
    public class UserFavoriteMovie : BaseEntity
    {
        public long UserId { get; set; }
        public long MovieId { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("MovieId")] 
        public Movie Movie { get; set; }
        #endregion
    }
}
