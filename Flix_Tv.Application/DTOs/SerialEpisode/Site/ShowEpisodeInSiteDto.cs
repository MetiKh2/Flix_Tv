using Flix_Tv.Domain.Entites.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DTOs.SerialEpisode.Site
{
  public  class ShowEpisodeInSiteDto
   {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ShowEpisodeFileInSiteDto> EpisodeFiles { get; set; }
        public string ImageName { get; set; }
        public string SerialName { get; set; }
        public long SerialId { get; set; }
        public double? AvvrageRate { get; set; }


    }
    public class ShowEpisodeFileInSiteDto
    {
        public Quality Quality { get; set; }
        public string FileName { get; set; }
    }
}
