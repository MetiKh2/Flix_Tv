using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.Serial.Site
{
   public class ShowSerialInSiteDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Tiser { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
        public short AgeRestriction { get; set; }
        public string Description { get; set; }
        public List<string> Categories { get; set; }
        public string ImageName { get; set; }
        public bool ExistUserFavoriteSerial { get; set; }
        public List<ShowSerialSeasonDto> SerialSeasons { get; set; }
        public double? AvvrageRate { get; set; }
        public List<ShowSerialEpisodesDto> SerialEpisodes { get; set; }

    }
    public class ShowSerialSeasonDto
    {
        public string SeasonTitle { get; set; }
        public long SeasonId { get; set; }

    }
    public class ShowSerialEpisodesDto
    {
        
        public string EpisodeTitle { get; set; }
        public string Time { get; set; }
        public string EpisodeImage { get; set; }
        public long EpisodeId { get; set; }
        public long SeasonId { get; set; }
        public double? AvvrageRate { get; set; }

    }
}
