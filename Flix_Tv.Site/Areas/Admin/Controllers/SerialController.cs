using Flix_Tv.Application.DTOs.Serial.Admin;
using Flix_Tv.Application.DTOs.SerialEpisode.Admin;
using Flix_Tv.Application.DTOs.SerialSeason.Admin;
using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Domain.Entites.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SerialController : Controller
    {
        private readonly ISerialService _serialService;
        public SerialController(ISerialService serialService)
        {
            _serialService = serialService;
        }
        [Route("Admin/Serials")]
        public async Task<IActionResult> Index(int pageId = 1, string sort = "date", string filter = "")
        {
            ViewBag.pageId = pageId;
            ViewBag.filter = filter;
            ViewBag.sort = sort;
            return View(await _serialService.GetSerialsInAdmin(pageId, filter, sort));
        }
        [Route("Admin/CreateSerial")]
        public async Task<IActionResult> CreateSerial()
        {
            ViewBag.SerialCategories = await _serialService.GetSerialCategories();
            return View();
        }
        [Route("Admin/CreateSerial")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 259715200)]
        public async Task<IActionResult> CreateSerial(CreateSerialDto dto, List<long> selectedSerialCategories)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SerialCategories = await _serialService.GetSerialCategories();
                return View(dto);
            }
            if (dto.Tiser != null && dto.Tiser.FileName.ToLower().EndsWith(".mp4") == false)
            {
                ViewBag.SerialCategories = await _serialService.GetSerialCategories();
                ModelState.AddModelError("Tiser", "پسوند تیزر باید mp4 باشد");
                return View(dto);
            }
            var serialId = await _serialService.CreateSerial(dto);
            if (selectedSerialCategories.Any()) await _serialService.CreateSerialCategorySerials(serialId, selectedSerialCategories);
            return Redirect("/Admin/Serials");
        }
        [HttpPost]
        [Route("Admin/ChengeSerialIsSliderStatus")]
        public async Task<IActionResult> ChengeSerialIsSliderStatus(long Id)
        {
            var serial = await _serialService.GetSerialById(Id);
            if (serial == null) return NotFound();
            serial.IsSlider = !serial.IsSlider;
            serial.UpdateDate = DateTime.Now;
            await _serialService.UpdateSerial(serial);
            ChangeIsSliderSerialStatusDto dto = new ChangeIsSliderSerialStatusDto() { Id = Id, IsSlider = serial.IsSlider };
            return Json(dto);
        }
        [HttpPost]
        [Route("Admin/ChengeSerialActivate")]
        public async Task<IActionResult> ChengeSerialActivate(long Id)
        {
            var serial = await _serialService.GetSerialById(Id);
            if (serial == null) return NotFound();
            serial.IsActive = !serial.IsActive;
            serial.UpdateDate = DateTime.Now;
            await _serialService.UpdateSerial(serial);
            ChengeSerialActivateDto dto = new ChengeSerialActivateDto() { Id = Id, IsActive = serial.IsActive };
            return Json(dto);
        }
        [HttpPost]
        [Route("Admin/DeleteSerial")]
        public async Task<IActionResult> DeleteSerial(long Id) => Json(await _serialService.DeleteSerial(Id));


        [Route("Admin/EditSerial/{id}")]
        public async Task<IActionResult> EditSerial(long id)
        {
            var serial = await _serialService.GetSerialById(id);
            if (serial == null) return NotFound();
            ViewBag.SerialCategories = await _serialService.GetSerialCategories();
            ViewBag.SerialCategorySerials = await _serialService.GetSerialCategoryIdSerials(id);
            return View(new EditSerialDto
            {
                Id = serial.Id,
                AgeRestriction = serial.AgeRestriction,
                Description = serial.Description,
                IsFree = serial.IsFree,
                LastImage = serial.Image,
                LastTiser = serial.Tiser,
                Title = serial.Title,
                YearOfCreateDate = serial.YearOfCreateDate,
                Tag = serial.Tag,
                Director=serial.Director

            });
        }

        [Route("Admin/EditSerial/{id}")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 259715200)]
        public async Task<IActionResult> EditSerial(EditSerialDto dto, List<long> selectedSerialCategories)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SerialCategories = await _serialService.GetSerialCategories();
                ViewBag.SerialCategorySerials = await _serialService.GetSerialCategoryIdSerials(dto.Id);
                return View(dto);
            }
            if (dto.Tiser != null && dto.Tiser.FileName.ToLower().EndsWith(".mp4") == false)
            {
                ViewBag.SerialCategories = await _serialService.GetSerialCategories();
                ViewBag.SerialCategorySerials = await _serialService.GetSerialCategoryIdSerials(dto.Id);
                ModelState.AddModelError("Tiser", "پسوند تیزر باید mp4 باشد");
                return View(dto);
            }
            await _serialService.EditSerial(dto);
            await _serialService.EditSerialCategorySerials(dto.Id, selectedSerialCategories);

            return Redirect("/Admin/Serials");
        }





        [Route("Admin/SerialSeasons/{serialId}")]
        public async Task<IActionResult> SerialSeasons(long serialId)
        {
            if (!await _serialService.ExistsSerial(serialId)) return NotFound();
            ViewBag.serialName = await _serialService.GetSerialTitleById(serialId);
            ViewBag.serialId = serialId;
            return View(await _serialService.GetSerialSeasonsInAdmin(serialId));
        }

        [Route("Admin/CreateSeason/{serialId}")]
        public async Task<IActionResult> CreateSeason(long serialId)
        {
            if (!await _serialService.ExistsSerial(serialId)) return NotFound();
            ViewBag.serialName = await _serialService.GetSerialTitleById(serialId);
            return View(new CreateSerialSeasonDto
            {
                SerialId = serialId
            });
        }
        [HttpPost]
        [Route("Admin/CreateSeason")]
        public async Task<IActionResult> CreateSeason(CreateSerialSeasonDto dto)
        {
            if (!await _serialService.ExistsSerial(dto.SerialId)) return NotFound();
            if (ModelState.IsValid == false)
            {
                ViewBag.serialName = await _serialService.GetSerialTitleById(dto.SerialId);
                return View(dto);
            }
            await _serialService.CreateSeason(dto);
            return Redirect("/admin/SerialSeasons/" + dto.SerialId);
        }
        [HttpPost]
        [Route("Admin/DeleteSeason")]
        public async Task<IActionResult> DeleteSeason(long Id)
        {
            return Json(
              await _serialService.DeleteSeason(Id)
            );
        }
        [Route("Admin/EditSeason/{Id}")]
        public async Task<IActionResult> EditSeason(long Id)
        {
            var seaoson = await _serialService.GetSerialSeasonById(Id);
            if (seaoson == null)
                return NotFound();
            return View(new EditSerialSeasonDto
            {
                Id = seaoson.Id,
                LastImage = seaoson.Image,
                Title = seaoson.Title,
                SerialId = seaoson.SerialId
            });
        }
        [Route("Admin/EditSeason")]
        [HttpPost]
        public async Task<IActionResult> EditSeason(EditSerialSeasonDto dto)
        {
            if (ModelState.IsValid == false)
            {
                return View(dto);
            }

            await _serialService.EditSeason(dto);
            return Redirect("/admin/SerialSeasons/" + dto.SerialId);
        }
        [Route("Admin/SerialEpisodes/{seasonId}")]
        public async Task<IActionResult> SerialEpisodes(long seasonId)
        {
            var season = await _serialService.GetSerialSeasonById(seasonId);
            if (season == null) return NotFound();

            ViewBag.serialName = await _serialService.GetSerialTitleById(season.SerialId);
            ViewBag.seasonName = season.Title;
            ViewBag.seasonId = seasonId;
            return View(await _serialService.GetSerialEpisodesInAdmin(seasonId));
        }
        [Route("Admin/CreateEpisode/{seasonId}")]
        public async Task<IActionResult> CreateEpisode(long seasonId)
        {
            var seaoson = await _serialService.GetSerialSeasonById(seasonId);
            if (seaoson == null) return NotFound();


            ViewBag.serialName = await _serialService.GetSerialTitleById(seaoson.SerialId);

            ViewBag.seasonName = seaoson.Title;
            return View(new CreateSerialEpisodeDto
            {
                SeasonId = seasonId,
                SerialId = await _serialService.GetSerialIdBySeasonId(seasonId),
            });
        }
        [HttpPost]
        [Route("Admin/CreateEpisode")]
        public async Task<IActionResult> CreateEpisode(CreateSerialEpisodeDto dto)
        {
            var seaoson = await _serialService.GetSerialSeasonById(dto.SeasonId);
            if (seaoson == null) return NotFound();
            if (!ModelState.IsValid)
            {
                ViewBag.serialName = await _serialService.GetSerialTitleById(seaoson.SerialId);
                ViewBag.seasonName = seaoson.Title;

                return View(dto);
            }
            await _serialService.CreateEpisode(dto);
            return Redirect("/Admin/SerialEpisodes/" + dto.SeasonId);
        }

        [HttpPost]
        [Route("Admin/DeleteEpisode")]
        public async Task<IActionResult> DeleteEpisode(long Id)
        {
            return Json(await _serialService.DeleteEpisode(Id)) ;
        }

        [Route("Admin/EditEpisode/{id}")]
        public async Task<IActionResult> EditEpisode(long id)
        {
            var episode =await _serialService.GetSerialEpisodeById(id);
            if (episode == null) return NotFound();

            ViewBag.serialName = await _serialService.GetSerialTitleById(episode.SerialId);
            ViewBag.seasonName = await _serialService.GetSeasonNameById(episode.SeasonId);

            return View(new EditSerialEpisodeDto { 
            Id=episode.Id,
            Description=episode.Description,
           LastImage=episode.Image,
           Title=episode.Title,
           SeasonId=episode.SeasonId
            });
        }
        [HttpPost]
        [Route("Admin/EditEpisode")]
        public async Task<IActionResult> EditEpisode(EditSerialEpisodeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
           await _serialService.EditEpisode(dto);
            return Redirect("/Admin/SerialEpisodes/"+dto.SeasonId);
        }


        [Route("Admin/CreateSerialEpisodeFile/{episodeId}")]
        public async Task<IActionResult> CreateSerialEpisodeFile(long episodeId)
        {
            if (!await _serialService.ExistsEpisode(episodeId)) return NotFound();
            ViewBag.episodeId = episodeId;
            ViewBag.serialName =await _serialService.GetSerialTitleById(await _serialService.GetSerialIdByEpisodeId(episodeId));
            return View();
        }
        [Route("Admin/CreateSerialEpisodeFile")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 4009715200)]
        public async Task<IActionResult> CreateSerialEpisodeFile(long episodeId,Quality? quality,IFormFile episodeFile)
        {
            if (!await _serialService.ExistsEpisode(episodeId)) return NotFound();
            ViewBag.episodeId = episodeId;
            ViewBag.serialName =await _serialService.GetSerialTitleById(await _serialService.GetSerialIdByEpisodeId(episodeId));
            if (episodeFile == null)
            {
                ViewBag.error = true;
                return View();
            }
            if (quality == null)
            {
                ViewBag.error = true;
                return View();
            }
            if (episodeFile.FileName.ToLower().EndsWith(".mp4") == false)
            {
                ViewBag.errorExtension = true;
                return View();
            }
            var pathName = await _serialService.CreateEpisodeFile(episodeId, episodeFile, quality.Value);
            ViewBag.Success = true;
            ViewBag.moviePath = pathName.Substring(39).Replace(@"\", "/");
            return View();
        }
        [Route("Admin/getEpisodeFile/{episodeId}")]
        public async Task<IActionResult> getEpisodeFile(long episodeId, Quality quality)
        {
            var episodeSrc = await _serialService.GetEpisodeFileSrcByEpisodeIdAndQuality(episodeId, quality);
            if (string.IsNullOrWhiteSpace(episodeSrc)) return NotFound();
            string qualityString = quality.ToString();
            if (quality != Quality.Q4k) qualityString += "p";
            ViewBag.fileName = episodeSrc;
            ViewBag.quality = qualityString.Substring(1);
            return View();
        }

        [Route("Admin/SerialComments/{serialId}")]
        public async Task<IActionResult> SerialComments(long serialId, int pageId = 1, string filter = "")
        {
            if (!await _serialService.ExistsSerial(serialId)) return NotFound();
            ViewBag.pageId = pageId;
            ViewBag.filter = filter;
            ViewBag.serialId = serialId;
            return View(await _serialService.ShowSerialCommentsInAdmin(serialId, pageId, filter));
        }
        [Route("Admin/ReplySerialComments/{parentId}")]
        public async Task<IActionResult> ReplySerialComments(long? parentId)
        {
            if (parentId == null) return NotFound();
            if (!await _serialService.ExistComment(parentId.Value)) return NotFound();
            return View(await _serialService.ShowSubSerialCommentsInAdmin(parentId));
        }
        [Route("DeleteSerialComment")]
        [HttpPost]
        public async Task<IActionResult> DeleteSerialComment(long id)
        {
            return Json(await _serialService.DeleteSerialComment(id));
        }


        [Route("Admin/EpisodeComments/{episodeId}")]
        public async Task<IActionResult> EpisodeComments(long episodeId, int pageId = 1, string filter = "")
        {
            if (!await _serialService.ExistsEpisode(episodeId)) return NotFound();
            ViewBag.pageId = pageId;
            ViewBag.filter = filter;
            ViewBag.episodeId = episodeId;
            return View(await _serialService.ShowEpisodeCommentsInAdmin(episodeId, pageId, filter));
        }
        [Route("Admin/ReplyEpisodeComments/{parentId}")]
        public async Task<IActionResult> ReplyEpisodeComments(long? parentId)
        {
            if (parentId == null) return NotFound();
            if (!await _serialService.ExistEpisodeComment(parentId.Value)) return NotFound();
            return View(await _serialService.ShowSubEpisodeCommentsInAdmin(parentId));
        }
        [Route("DeleteEpisodeComment")]
        [HttpPost]
        public async Task<IActionResult> DeleteEpisodeComment(long id)
        {
            return Json(await _serialService.DeleteEpisodeComment(id));
        }
    }
}

