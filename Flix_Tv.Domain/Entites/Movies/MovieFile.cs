using Flix_Tv.Domain.Entites.BaseEntities;
using Flix_Tv.Domain.Entites.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Movies
{
   public class MovieFile:BaseEntity
    {
        public long MovieId { get; set; }
        public Quality Quality { get; set; }
        public string FileName { get; set; }

        #region Relations
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
        #endregion
    }
}
