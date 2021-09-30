using Flix_Tv.Domain.Entites.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Movies
{
  public  class MovieCategoryMovie:BaseEntity
    {
        public long MovieId { get; set; }
        public long CategoryId { get; set; }

        #region Relations
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
        [ForeignKey("CategoryId")]
        public MovieCategory Category { get; set; }

        #endregion
    }
}
