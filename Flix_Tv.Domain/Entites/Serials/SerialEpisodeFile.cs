using Flix_Tv.Domain.Entites.BaseEntities;
using Flix_Tv.Domain.Entites.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Domain.Entites.Serials
{
   public class SerialEpisodeFile:BaseEntity
    {
        public long SerialEpisodeId { get; set; }
        public Quality Quality { get; set; }
        public string FileName { get; set; }

        #region Relations
        [ForeignKey("SerialEpisodeId")]
        public SerialEpisode SerialEpisode { get; set; }
        #endregion
    }
}
