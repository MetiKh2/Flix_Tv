using Flix_Tv.Application.DatabaseInterfaces;
using Flix_Tv.Application.DTOs.AdminPage;
using Flix_Tv.Application.DTOs.Movie;
using Flix_Tv.Application.DTOs.Public;
using Flix_Tv.Application.DTOs.Serial.Admin;
using Flix_Tv.Application.DTOs.Serial.Site;
using Flix_Tv.Application.DTOs.SerialCategory.Admin;
using Flix_Tv.Application.DTOs.SerialCategory.Admin.Edit;
using Flix_Tv.Application.DTOs.SerialCategory.Admin.Serials;
using Flix_Tv.Application.DTOs.SerialComment.Site;
using Flix_Tv.Application.DTOs.SerialEpisode.Admin;
using Flix_Tv.Application.DTOs.SerialEpisode.Site;
using Flix_Tv.Application.DTOs.SerialSeason.Admin;
using Flix_Tv.Application.DTOs.User.Profile;
using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Common.Convertors;
using Flix_Tv.Common.Generators;
using Flix_Tv.Common.Security;
using Flix_Tv.Domain.Entites.Enums;
using Flix_Tv.Domain.Entites.Serials;
using Flix_Tv.Domain.Entites.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.Services
{
    public class SerialService : ISerialService
    {
        private readonly IFlixTvContext _context;
        private readonly IUserService _userService;
        public SerialService(IFlixTvContext context, IUserService userService)
        {
            _userService = userService;
            _context = context;
        }

        public async Task CreateEpisode(CreateSerialEpisodeDto dto)
        {
            var episode = new SerialEpisode
            {
                CraeteDate = DateTime.Now,
                Description = dto.Description,
                SeasonId = dto.SeasonId,
                SerialId = dto.SerialId,
                Title = dto.Title,
            };
            if (dto.Image != null && dto.Image.IsImage())
            {
                episode.Image = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.Image.FileName);
                SaveFileInServer save = new SaveFileInServer();
                var imagePath = await save.SaveFile("wwwroot/Serials/Episodes/Images", episode.Image, dto.Image);
                string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Episodes/ThumbImages", episode.Image);
                ImageResizer resizer = new ImageResizer();
                resizer.Image_resize(imagePath, resizeImageName, 300);
            }
            await _context.SerialEpisodes.AddAsync(episode);
            await _context.SaveChangesAsync();
        }

        public async Task<ShowSerialCommentDto> CreateEpisodeComment(string text, string subject, short? rate, long userId, long episodeId, long? parentId)
        {

            var comment = new SerialEpisodeComment
            {
                CraeteDate = DateTime.Now,
                SerialEpisodeId = episodeId,
                Rate = rate,
                Text = text,
                Title = subject,
                UserId = userId
           ,
                ParentId = parentId,
            };

            if (rate == null) rate = 0;
            await _context.SerialEpisodeComments.AddAsync(comment);
            await _context.SaveChangesAsync();
            var result =
             new ShowSerialCommentDto
             {
                 CreateDate = DateTime.Now.ToShamsi(),
                 Rate = rate,
                 Subject = subject,
                 Text = text,
                 UserName = await _userService.GetUserNameById(userId),
                 Id = comment.Id,
                 UserImage = _userService.GetUserImageByIdNoAsync(userId),

             };
            if (parentId != null && await ExistComment(parentId.Value))
            {
                result.ParentText = await GetEpisodeCommentTextById(parentId.Value);
            }
            return result;
        }

        public async Task<string> CreateEpisodeFile(long episodeId, IFormFile episodeFile, Quality quality)
        {
            if (episodeFile.FileName.ToLower().EndsWith(".mp4") == false) return null;
            var newEpisodeFile = new SerialEpisodeFile
            {
                CraeteDate = DateTime.Now,
                SerialEpisodeId = episodeId,
                Quality = quality,
                FileName = GeneratCode.GenerateUniqCode() + quality + Path.GetExtension(episodeFile.FileName),
            };
            string pathName = null;
            SaveFileInServer saveFile = new SaveFileInServer();
            switch (quality)
            {
                case Quality.Q240:
                    if (await _context.SerialEpisodeFiles.AnyAsync(p => p.SerialEpisodeId == episodeId && p.Quality == Quality.Q240))
                    {
                        var lastEpisodeFile = await _context.SerialEpisodeFiles.Where(p => p.SerialEpisodeId == episodeId && p.Quality == Quality.Q240).FirstOrDefaultAsync();
                        var lastEpisodeFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Files/240p", lastEpisodeFile.FileName);
                        if (File.Exists(lastEpisodeFilePath)) File.Delete(lastEpisodeFilePath);
                        lastEpisodeFile.IsRemoved = true;
                        lastEpisodeFile.RemoveDate = DateTime.Now;
                    }
                    pathName = await saveFile.SaveFile("wwwroot/Serials/Files/240p", newEpisodeFile.FileName, episodeFile);
                    break;
                case Quality.Q360:
                    if (await _context.SerialEpisodeFiles.AnyAsync(p => p.SerialEpisodeId == episodeId && p.Quality == Quality.Q360))
                    {
                        var lastEpisodeFile = await _context.SerialEpisodeFiles.Where(p => p.SerialEpisodeId == episodeId && p.Quality == Quality.Q360).FirstOrDefaultAsync();
                        var lastEpisodeFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Files/360p", lastEpisodeFile.FileName);
                        if (File.Exists(lastEpisodeFilePath)) File.Delete(lastEpisodeFilePath);
                        lastEpisodeFile.IsRemoved = true;
                        lastEpisodeFile.RemoveDate = DateTime.Now;
                    }
                    pathName = await saveFile.SaveFile("wwwroot/Serials/Files/360p", newEpisodeFile.FileName, episodeFile);
                    break;
                case Quality.Q480:
                    if (await _context.SerialEpisodeFiles.AnyAsync(p => p.SerialEpisodeId == episodeId && p.Quality == Quality.Q480))
                    {
                        var lastEpisodeFile = await _context.SerialEpisodeFiles.Where(p => p.SerialEpisodeId == episodeId && p.Quality == Quality.Q480).FirstOrDefaultAsync();
                        var lastEpisodeFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Files/480p", lastEpisodeFile.FileName);
                        if (File.Exists(lastEpisodeFilePath)) File.Delete(lastEpisodeFilePath);
                        lastEpisodeFile.IsRemoved = true;
                        lastEpisodeFile.RemoveDate = DateTime.Now;
                    }
                    pathName = await saveFile.SaveFile("wwwroot/Serials/Files/480p", newEpisodeFile.FileName, episodeFile);
                    break;
                case Quality.Q720:
                    if (await _context.SerialEpisodeFiles.AnyAsync(p => p.SerialEpisodeId == episodeId && p.Quality == Quality.Q720))
                    {
                        var lastEpisodeFile = await _context.SerialEpisodeFiles.Where(p => p.SerialEpisodeId == episodeId && p.Quality == Quality.Q720).FirstOrDefaultAsync();
                        var lastEpisodeFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Files/720p", lastEpisodeFile.FileName);
                        if (File.Exists(lastEpisodeFilePath)) File.Delete(lastEpisodeFilePath);
                        lastEpisodeFile.IsRemoved = true;
                        lastEpisodeFile.RemoveDate = DateTime.Now;
                    }
                    pathName = await saveFile.SaveFile("wwwroot/Serials/Files/720p", newEpisodeFile.FileName, episodeFile);
                    break;
                case Quality.Q1080:
                    if (await _context.SerialEpisodeFiles.AnyAsync(p => p.SerialEpisodeId == episodeId && p.Quality == Quality.Q1080))
                    {
                        var lastEpisodeFile = await _context.SerialEpisodeFiles.Where(p => p.SerialEpisodeId == episodeId && p.Quality == Quality.Q1080).FirstOrDefaultAsync();
                        var lastEpisodeFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Files/1080p", lastEpisodeFile.FileName);
                        if (File.Exists(lastEpisodeFilePath)) File.Delete(lastEpisodeFilePath);
                        lastEpisodeFile.IsRemoved = true;
                        lastEpisodeFile.RemoveDate = DateTime.Now;
                    }
                    pathName = await saveFile.SaveFile("wwwroot/Serials/Files/1080p", newEpisodeFile.FileName, episodeFile);
                    break;
                case Quality.Q4k:
                    if (await _context.SerialEpisodeFiles.AnyAsync(p => p.SerialEpisodeId == episodeId && p.Quality == Quality.Q4k))
                    {
                        var lastEpisodeFile = await _context.SerialEpisodeFiles.Where(p => p.SerialEpisodeId == episodeId && p.Quality == Quality.Q4k).FirstOrDefaultAsync();
                        var lastEpisodeFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Files/4k", lastEpisodeFile.FileName);
                        if (File.Exists(lastEpisodeFilePath)) File.Delete(lastEpisodeFilePath);
                        lastEpisodeFile.IsRemoved = true;
                        lastEpisodeFile.RemoveDate = DateTime.Now;
                    }
                    pathName = await saveFile.SaveFile("wwwroot/Serials/Files/4k", newEpisodeFile.FileName, episodeFile);
                    break;

            }
            await _context.SerialEpisodeFiles.AddAsync(newEpisodeFile);
            await _context.SaveChangesAsync();
            return pathName;
        }

        public async Task CreateSeason(CreateSerialSeasonDto dto)
        {
            var season = new SerialSeason
            {
                CraeteDate = DateTime.Now,
                SerialId = dto.SerialId,
                Title = dto.Title,
            };
            if (dto.Image != null && dto.Image.IsImage())
            {
                season.Image = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.Image.FileName);
                SaveFileInServer save = new SaveFileInServer();
                var imagePath = await save.SaveFile("wwwroot/Serials/Seasons/Images", season.Image, dto.Image);
                string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Seasons/ThumbImages", season.Image);
                ImageResizer resizer = new ImageResizer();
                resizer.Image_resize(imagePath, resizeImageName, 300);
            }
            await _context.SerialSeasons.AddAsync(season);
            await _context.SaveChangesAsync();
        }


        public async Task<long> CreateSerial(CreateSerialDto dto)
        {
            var serial = new Serial
            {
                IsFree = dto.IsFree,
                AgeRestriction = dto.AgeRestriction,
                CraeteDate = DateTime.Now,
                Description = dto.Description,
                Tag = dto.Tag,
                Title = dto.Title,
                YearOfCreateDate = dto.YearOfCreateDate,
                Director = dto.Director
            };
            if (dto.Image != null && dto.Image.IsImage())
            {
                serial.Image = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.Image.FileName);
                SaveFileInServer saveFile = new SaveFileInServer();
                var pathName = await saveFile.SaveFile("wwwroot/Serials/Images", serial.Image, dto.Image);
                ImageResizer resizer = new ImageResizer();
                string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/ThumbImages", serial.Image);
                resizer.Image_resize(pathName, resizeImageName, 350);
            }
            if (dto.Tiser != null && dto.Tiser.IsMovie())
            {
                serial.Tiser = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.Tiser.FileName);
                SaveFileInServer saveFile = new SaveFileInServer();
                await saveFile.SaveFile("wwwroot/Serials/Tisers", serial.Tiser, dto.Tiser);
            }
            await _context.Serials.AddAsync(serial);
            await _context.SaveChangesAsync();
            return serial.Id;
        }

        public async Task<SerialCategory> CreateSerialCategory(string name, IFormFile image)
        {
            var serialCategory = new SerialCategory
            {
                CraeteDate = DateTime.Now,
                Title = name
            };
            if (image != null && image.IsImage())
            {
                serialCategory.ImageSrc = GeneratCode.GenerateUniqCode() + Path.GetExtension(image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Categories/Images", serialCategory.ImageSrc);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                ImageResizer resizer = new ImageResizer();
                string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Categories/ThumbImages", serialCategory.ImageSrc);
                resizer.Image_resize(imagePath, resizeImageName, 350);
            }
            await _context.SerialCategories.AddAsync(serialCategory);
            await _context.SaveChangesAsync();
            return serialCategory;
        }

        public async Task CreateSerialCategorySerials(long serialId, List<long> selectedSerialCategories)
        {
            foreach (var item in selectedSerialCategories)
            {
                await _context.SerialCategorySerials.AddAsync(new SerialCategorySerial
                {
                    SerialCategoryId = item,
                    CraeteDate = DateTime.Now,
                    SerialId = serialId,
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task<ShowSerialCommentDto> CreateSerialComment(string text, string subject, short? rate, string userName, long serialId, long? parentId)
        {
            var userId = await _userService.GetUserIdByUserName(userName);
            var comment = new SerialComment
            {
                CraeteDate = DateTime.Now,
                SerialId = serialId,
                Rate = rate,
                Text = text,
                Title = subject,
                UserId = userId
           ,
                ParentId = parentId,
            };

            if (rate == null) rate = 0;
            await _context.SerialComments.AddAsync(comment);
            await _context.SaveChangesAsync();
            var result =
             new ShowSerialCommentDto
             {
                 CreateDate = DateTime.Now.ToShamsi(),
                 Rate = rate,
                 Subject = subject,
                 Text = text,
                 UserName = userName,
                 Id = comment.Id,
                 UserImage = _userService.GetUserImageByIdNoAsync(userId),

             };
            if (parentId != null && await ExistComment(parentId.Value))
            {
                result.ParentText = await GetCommentTextById(parentId.Value);
            }
            return result;
        }

        public async Task CreateUserFavoriteSerial(long userId, long serialId)
        {
            var userFavoriteSerial = new UserFavoriteSerial
            {
                UserId = userId,
                CraeteDate = DateTime.Now,
                SerialId = serialId,
            };
            await _context.UserFavoriteSerials.AddAsync(userFavoriteSerial);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteEpisode(long id)
        {
            try
            {
                var episode = await GetSerialEpisodeById(id);
                if (episode != null)
                {
                    episode.IsRemoved = true;
                    episode.RemoveDate = DateTime.Now;
                    await UpdateEpisdoe(episode);
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> DeleteSeason(long id)
        {
            try
            {
                var season = await GetSerialSeasonById(id);
                if (season != null)
                {
                    season.IsRemoved = true;
                    season.RemoveDate = DateTime.Now;
                    await UpdateSeason(season);
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> DeleteSerial(long id)
        {
            try
            {
                var movie = await GetSerialById(id);
                if (movie != null)
                {
                    movie.IsRemoved = true;
                    movie.RemoveDate = DateTime.Now;
                    // await DeleteMovieCategoryMovies(id);
                    await UpdateSerial(movie);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public async Task<bool> DeleteSerialCategory(long id)
        {
            try
            {
                var category = await GetSerialCategoryById(id);
                if (category != null)
                {
                    category.IsRemoved = true;
                    category.RemoveDate = DateTime.Now;
                    await UpdateSerialCategory(category);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
                throw;
            }
        }

        public async Task DeleteSerialCategorySerial(long serialId)
        {
            var serialCategroySerials = await _context.SerialCategorySerials.Where(p => p.SerialId == serialId).ToListAsync();
            foreach (var item in serialCategroySerials)
            {
                item.IsRemoved = true;
                item.RemoveDate = DateTime.Now;
            }
        }

        public async Task<bool> DeleteSerialComment(long id)
        {
            if (await ExistComment(id))
            {
                var comment = await _context.SerialComments.FindAsync(id);
                comment.IsRemoved = true;
                comment.RemoveDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task DeleteUserFavoriteSerial(long userId, long serialId)
        {
            var userFavoriteSerial = await _context.UserFavoriteSerials.Where(p => p.SerialId == serialId && p.UserId == userId).FirstOrDefaultAsync();
            userFavoriteSerial.IsRemoved = true;
            userFavoriteSerial.RemoveDate = DateTime.Now;
            _context.UserFavoriteSerials.Update(userFavoriteSerial);
            await _context.SaveChangesAsync();
        }

        public async Task EditEpisode(EditSerialEpisodeDto dto)
        {
            var episode = await GetSerialEpisodeById(dto.Id);
            if (episode != null)
            {
                episode.Description = dto.Description;
                episode.Title = dto.Title;
                if (dto.Image != null && dto.Image.IsImage())
                {
                    if (episode.Image != null)
                    {
                        var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Episodes/Images", episode.Image);
                        if (File.Exists(deleteImagePath))
                        {
                            File.Delete(deleteImagePath);
                        }
                        var deleteThumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Episodes/ThumbImages", episode.Image);
                        if (File.Exists(deleteThumbImagePath))
                        {
                            File.Delete(deleteThumbImagePath);
                        }
                    }
                    episode.Image = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.Image.FileName);
                    SaveFileInServer save = new SaveFileInServer();
                    var imagePath = await save.SaveFile("wwwroot/Serials/Episodes/Images", episode.Image, dto.Image);
                    string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Episodes/ThumbImages", episode.Image);
                    ImageResizer resizer = new ImageResizer();
                    resizer.Image_resize(imagePath, resizeImageName, 300);
                }
                await UpdateEpisdoe(episode);
            }
        }

        public async Task EditSeason(EditSerialSeasonDto dto)
        {
            var season = await GetSerialSeasonById(dto.Id);
            if (season != null)
            {
                season.Title = dto.Title;
                if (dto.Image != null && dto.Image.IsImage())
                {
                    if (season.Image != null)
                    {
                        var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Seasons/Images", season.Image);
                        if (File.Exists(deleteImagePath))
                        {
                            File.Delete(deleteImagePath);
                        }
                        var deleteThumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Seasons/ThumbImages", season.Image);
                        if (File.Exists(deleteThumbImagePath))
                        {
                            File.Delete(deleteThumbImagePath);
                        }
                    }
                    season.Image = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.Image.FileName);
                    SaveFileInServer save = new SaveFileInServer();
                    var imagePath = await save.SaveFile("wwwroot/Serials/Seasons/Images", season.Image, dto.Image);
                    string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Seasons/ThumbImages", season.Image);
                    ImageResizer resizer = new ImageResizer();
                    resizer.Image_resize(imagePath, resizeImageName, 300);
                }
                await UpdateSeason(season);
            }
        }

        public async Task EditSerial(EditSerialDto dto)
        {
            var serial = await GetSerialById(dto.Id);
            if (serial != null)
            {
                serial.AgeRestriction = dto.AgeRestriction;
                serial.Title = dto.Title;
                serial.YearOfCreateDate = dto.YearOfCreateDate;
                serial.Description = dto.Description;
                serial.IsFree = dto.IsFree;
                serial.Tag = dto.Tag;
                serial.UpdateDate = DateTime.Now;
                serial.Director = dto.Director;
                if (dto.Image != null && dto.Image.IsImage())
                {
                    if (!string.IsNullOrEmpty(serial.Image))
                    {
                        var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Images", serial.Image);
                        var deleteThumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/ThumbImages", serial.Image);
                        if (File.Exists(deleteImagePath)) File.Delete(deleteImagePath);
                        if (File.Exists(deleteThumbImagePath)) File.Delete(deleteThumbImagePath);
                    }
                    serial.Image = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.Image.FileName);
                    var saveFile = new SaveFileInServer();
                    var imagePath = await saveFile.SaveFile("wwwroot/Serials/Images", serial.Image, dto.Image);
                    ImageResizer resizer = new ImageResizer();
                    string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/ThumbImages", serial.Image);
                    resizer.Image_resize(imagePath, resizeImageName, 350);
                }
                if (dto.Tiser != null && dto.Tiser.IsMovie())
                {
                    if (!string.IsNullOrEmpty(serial.Tiser))
                    {
                        var deleteTiserPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Tisers", serial.Tiser);
                        if (File.Exists(deleteTiserPath)) File.Delete(deleteTiserPath);
                    }
                    serial.Tiser = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.Tiser.FileName);
                    var saveFile = new SaveFileInServer();
                    var tiserPath = await saveFile.SaveFile("wwwroot/Serials/Tisers", serial.Tiser, dto.Tiser);
                }
                await UpdateSerial(serial);
                //1
            }
        }


        public async Task EditSerialCategory(SerialCategoryForEditDto dto, IFormFile image)
        {
            var category = await GetSerialCategoryById(dto.Id);
            category.Title = dto.Title;
            if (image != null && image.IsImage())
            {
                if (!string.IsNullOrEmpty(category.ImageSrc))
                {
                    var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Categories/Images", category.ImageSrc);
                    var deleteThumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Categories/ThumbImages", category.ImageSrc);
                    if (File.Exists(deleteImagePath)) File.Delete(deleteImagePath);
                    if (File.Exists(deleteThumbImagePath)) File.Delete(deleteThumbImagePath);
                }

                category.ImageSrc = GeneratCode.GenerateUniqCode() + Path.GetExtension(image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Categories/Images", category.ImageSrc);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                ImageResizer resizer = new ImageResizer();
                string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Serials/Categories/ThumbImages", category.ImageSrc);
                resizer.Image_resize(imagePath, resizeImageName, 350);
            }
            await UpdateSerialCategory(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditSerialCategorySerials(long serialId, List<long> selectedSerialCategories)
        {
            await DeleteSerialCategorySerial(serialId);
            await CreateSerialCategorySerials(serialId, selectedSerialCategories);
        }

        public async Task<bool> ExistComment(long id)
        {
            return await _context.SerialComments.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> ExistsEpisode(long id)
        {
            return await _context.SerialEpisodes.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> ExistsSerial(long serialId)
        {
            return await _context.Serials.AnyAsync(p => p.Id == serialId);
        }

        public async Task<bool> ExistsSerialCategory(long id)
        {
            return await _context.SerialCategories.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> ExistsUserEpisodeVote(long episodeId, long userId)
        {
            return await _context.SerialEpisodeComments.AnyAsync(p => p.SerialEpisodeId == episodeId && p.UserId == userId && p.Rate != null);
        }

        public async Task<bool> ExistsUserFavoriteSerial(long userId, long serialId)
        {
            return await _context.UserFavoriteSerials.AnyAsync(p => p.UserId == userId && p.SerialId == serialId);
        }

        public async Task<bool> ExistsUserSerialVote(long serialId, string username)
        {
            var userId = await _userService.GetUserIdByUserName(username);
            return await _context.SerialComments.AnyAsync(p => p.SerialId == serialId && p.UserId == userId && p.Rate != null);
        }

        public async Task<bool> ExsistSeason(long id)
        {
            return await _context.SerialSeasons.AnyAsync(p => p.Id == id);
        }

        public async Task<string> GetCommentTextById(long id)
        {
            return await _context.SerialComments.Where(p => p.Id == id).Select(p => p.Text).FirstOrDefaultAsync();
        }

        public async Task<SerialEpisode> GetEpisodeById(long Id)
        {
            return await _context.SerialEpisodes.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<string> GetEpisodeCommentTextById(long id)
        {
            return await _context.SerialEpisodeComments.Where(p => p.Id == id).Select(p => p.Text).FirstOrDefaultAsync();
        }

        public async Task<string> GetEpisodeFileSrcByEpisodeIdAndQuality(long episodeId, Quality quality)
        {
            return await _context.SerialEpisodeFiles.Where(p => p.SerialEpisodeId == episodeId && p.Quality == quality).Select(p => p.FileName).FirstOrDefaultAsync();
        }

        public async Task<long> GetEpisodeIdByFileName(string fileName)
        {
            return await _context.SerialEpisodeFiles.Where(p => p.FileName == fileName).Select(p => p.SerialEpisodeId).FirstOrDefaultAsync();
        }

        public async Task<List<GetFavoriteMediasDto>> GetFavoriteSerials(long userId)
        {
            return await _context.UserFavoriteSerials.Include(p => p.Serial).ThenInclude(p => p.SerialCategorySerials).ThenInclude(p => p.SerialCategory).Where(p => p.UserId == userId).OrderByDescending(p => p.CraeteDate).Select(p => new GetFavoriteMediasDto
            {
                Id = p.Serial.Id,
                FirstCategory = p.Serial.SerialCategorySerials.FirstOrDefault().SerialCategory.Title,
                Title = p.Serial.Title,
                Image = p.Serial.Image,
                IsFree = p.Serial.IsFree,
                IsSerial = true,
                Year = p.Serial.YearOfCreateDate
            }).ToListAsync();
        }

        public async Task<bool> GetIsActiveSerial(long id)
        {
            return await _context.Serials.Where(p => p.Id == id).Select(p => p.IsActive).FirstOrDefaultAsync();
        }

        public async Task<List<GetIsSliderMediaDto>> GetIsSliderSerials()
        {
            return await _context.Serials.Include(p=>p.SerialComments).Include(p => p.SerialCategorySerials).ThenInclude(p => p.SerialCategory).Where(p => p.IsSlider && p.IsActive).OrderBy(p => p.CraeteDate).Select(p => new GetIsSliderMediaDto
            {
                Id = p.Id,
                FirstCategory = p.SerialCategorySerials.FirstOrDefault().SerialCategory.Title,
                Title = p.Title,
                IsFree = p.IsFree,
                YearOfCreate = p.YearOfCreateDate,
                ImageName = p.Image,
                IsSerial = true,
                AvvrageRate = p.SerialComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

            }).ToListAsync();
        }

        public async Task<List<GetLastMediasDto>> GetLastSerials(int take)
        {
            return await _context.Serials.Include(p=>p.SerialComments).Include(p => p.SerialCategorySerials).ThenInclude(p => p.SerialCategory).Where(p => p.IsActive).OrderByDescending(p => p.CraeteDate).Select(p => new GetLastMediasDto
            {
                Id = p.Id,
                FirstCategory = p.SerialCategorySerials.FirstOrDefault().SerialCategory.Title,
                Title = p.Title,
                IsFree = p.IsFree,
                Year = p.YearOfCreateDate,
                ImageName = p.Image,
                IsSerial = true,
                Director = p.Director,
                DateTime = p.CraeteDate,
                AvvrageRate = p.SerialComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

            }).Take(take).ToListAsync();
        }

        public async Task<List<GetLastUserCommentsDto>> GetLastUserEpisodeComments(long userId, int take)
        {
            return await _context.SerialEpisodeComments.Include(p => p.SerialEpisode).ThenInclude(p => p.Serial).Where(p => p.UserId == userId).OrderByDescending(p => p.CraeteDate).Take(take).Select(p => new GetLastUserCommentsDto
            {
                Id = p.Id,
                MediaTitle = p.SerialEpisode.Serial.Title,
                rate = p.Rate,
                Subject = p.Title,
                CreateDate = p.CraeteDate
            }).ToListAsync();
        }

        public async Task<List<GetLastUserCommentsDto>> GetLastUserSerialComments(long userId, int take)
        {
            return await _context.SerialComments.Include(p => p.Serial).Where(p => p.UserId == userId).OrderByDescending(p => p.CraeteDate).Take(take).Select(p => new GetLastUserCommentsDto
            {
                Id = p.Id,
                MediaTitle = p.Serial.Title,
                rate = p.Rate,
                Subject = p.Title,
                CreateDate = p.CraeteDate
            }).ToListAsync();
        }

        public async Task<List<GetSerialsInSiteDto>> GetMaybeLikeSerials(long serialId)
        {
            var serialCategriesId = await GetSerialCategoryIdSerials(serialId);
            var result = new List<GetSerialsInSiteDto>();
            foreach (var item in serialCategriesId)
            {
                var serials= await GetSerialsBySerialCategoryId(item);
              
                foreach (var serial in serials)
                {
                    
                    if (!result.Any(p => p.Id == serial.Id) && serial.IsActive)
                    {
                        result.Add(new GetSerialsInSiteDto
                        {
                            Id = serial.Id,
                            DateTime = serial.CraeteDate,
                            FirstCategory = GetSerialCategoryTitleByIdNoAsync(item),
                            ImageName = serial.Image,
                            IsFree = serial.IsFree,
                            Title = serial.Title,
                            Year = serial.YearOfCreateDate,
                            AvvrageRate =serial.SerialComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((serial.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? serial.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

                        });
                    }

                }

            }
            return result;
        }

        public async Task<string> GetSeasonNameById(long id)
        {
            return await _context.SerialSeasons.Where(p => p.Id == id).Select(p => p.Title).FirstOrDefaultAsync();
        }


        public async Task<Serial> GetSerialById(long id)
        {
            return await _context.Serials.FindAsync(id);
        }

        public async Task<List<GetSerialCategoriesForAdd_EditSerialsDto>> GetSerialCategories()
        {
            return await _context.SerialCategories.OrderByDescending(p => p.CraeteDate).Select(p => new GetSerialCategoriesForAdd_EditSerialsDto
            {
                Id = p.Id,
                Title = p.Title
            }).ToListAsync();
        }

        public async Task<List<GetMediaCategoriesDto>> GetSerialCategoriesForSite()
        {
            return await _context.SerialCategories.Include(p => p.SerialCategorySerials).Where(p => p.ImageSrc != null).Select(p => new GetMediaCategoriesDto
            {
                Id = p.Id,
                Image = p.ImageSrc,
                IsSerial = true,
                MediaCount = p.SerialCategorySerials.Where(x => x.Serial.IsActive).Count(),
                Title = p.Title
            }).ToListAsync();
        }

        public async Task<Tuple<List<GetSerialCategoriesDto>, int, int>> GetSerialCategoriesInAdmin(int pageId = 1, string filter = "")
        {
            var serialCategories = _context.SerialCategories.OrderByDescending(p => p.Id).AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                serialCategories = serialCategories.Where(p => p.Title.Contains(filter)).AsQueryable();
            }
            int take = 3;
            int skip = (pageId - 1) * take;
            return Tuple.Create(await serialCategories.Select(p => new GetSerialCategoriesDto
            {
                DateTime = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                Image = p.ImageSrc,
                Title = p.Title
            }).Skip(skip).Take(take).ToListAsync(), serialCategories.Count() / take, serialCategories.Count());
        }

        public async Task<SerialCategory> GetSerialCategoryById(long id)
        {
            return await _context.SerialCategories.FindAsync(id);
        }

        public async Task<List<long>> GetSerialCategoryIdSerials(long serialId)
        {
            return await _context.SerialCategorySerials.Where(p => p.SerialId == serialId).Select(p => p.SerialCategoryId).ToListAsync();
        }

        public async Task<string> GetSerialCategoryTitleById(long id)
        {
            return await _context.SerialCategories.Where(p => p.Id == id).Select(p => p.Title).FirstOrDefaultAsync();
        }

        public string GetSerialCategoryTitleByIdNoAsync(long id)
        {
            return _context.SerialCategories.Find(id).Title;
        }

        public async Task<SerialEpisode> GetSerialEpisodeById(long Id)
        {
            return await _context.SerialEpisodes.FindAsync(Id);
        }

        public async Task<List<GetSerialEpisodesInAdminDto>> GetSerialEpisodesInAdmin(long seasonId)
        {
            return await _context.SerialEpisodes.Where(p => p.SeasonId == seasonId).OrderByDescending(p => p.CraeteDate).Select(p => new GetSerialEpisodesInAdminDto
            {
                Id = p.Id,
                Image = p.Image,
                Title = p.Title,
                CreateDate = p.CraeteDate.ToShamsi()
            }).ToListAsync();
        }

        public async Task<long> GetSerialIdByEpisodeId(long episodeId)
        {
            return await _context.SerialEpisodes.Where(p => p.Id == episodeId).Select(p => p.SerialId).FirstOrDefaultAsync();
        }

        public async Task<long> GetSerialIdBySeasonId(long seasonId)
        {
            return await _context.SerialSeasons.Where(p => p.Id == seasonId).Select(p => p.SerialId).FirstOrDefaultAsync();
        }

        public async Task<List<Serial>> GetSerialsBySerialCategoryId(long categoryId)
        {
            return await _context.SerialCategorySerials.Include(p => p.Serial).ThenInclude(p=>p.SerialComments).Where(p => p.SerialCategoryId == categoryId).Select(p => p.Serial).ToListAsync(); ;
        }

        public async Task<SerialSeason> GetSerialSeasonById(long id)
        {
            return await _context.SerialSeasons.FindAsync(id);
        }

        public async Task<List<GetSerialSeasonsInAdminDto>> GetSerialSeasonsInAdmin(long serialId)
        {
            return await _context.SerialSeasons.Where(p => p.SerialId == serialId).OrderByDescending(p => p.CraeteDate).Select(p => new GetSerialSeasonsInAdminDto
            {
                CreateDate = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                Image = p.Image,
                Title = p.Title
            }).ToListAsync();
        }

        public async Task<Tuple<List<GetSerialsInAdminDto>, int, int>> GetSerialsInAdmin(int pageId = 1, string filter = "", string sort = "")
        {
            var serials = _context.Serials.Include(p=>p.SerialComments).OrderByDescending(p => p.CraeteDate).AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                serials = serials.Where(p => p.Title.Contains(filter)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "date":
                        {
                            break;
                        }
                    case "viewCount":
                        {
                            serials = serials.OrderByDescending(p => p.ViewCount).AsQueryable();
                            break;
                        }
                    case "rate":
                        {
                            serials = serials.OrderByDescending(p => p.SerialComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) / ((p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)).AsQueryable();
                            break;
                        }
                }
            }
            int take = 4;
            int skip = (pageId - 1) * take;

            return Tuple.Create(await serials.Select(p => new GetSerialsInAdminDto
            {
                AgeRestriction = p.AgeRestriction,
                Image = p.Image,
                IsFree = p.IsFree,
                YearOfCreateDate = p.YearOfCreateDate,
                Title = p.Title,
                ViewCount = p.ViewCount,
                CreateDate = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                IsActive = p.IsActive,
                IsSlider = p.IsSlider,
                Director = p.Director,
                AvvrageRate = p.SerialComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

            }).Skip(skip).Take(take).ToListAsync(), serials.Count() / take, serials.Count());
        }

        public async Task<Tuple<List<GetSerialsInSiteDto>, int>> GetSerialsInSite(int pageId = 1, string filter = "", long categoryId = 0, long year = 0, string sort = "date")
        {
            var serials = _context.Serials.Include(p=>p.SerialComments).Include(p => p.SerialCategorySerials).ThenInclude(p => p.SerialCategory).Where(p => p.IsActive).OrderByDescending(p => p.CraeteDate).AsQueryable();
            if (await ExistsSerialCategory(categoryId))
            {
                var serialCategorySerials = await _context.SerialCategorySerials.Where(p => p.SerialCategoryId == categoryId).Select(p => p.SerialId).ToListAsync();
                serials = serials.Where(p => serialCategorySerials.Contains(p.Id)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(filter))
                serials = serials.Where(p => p.Title.Contains(filter) || p.YearOfCreateDate.ToString().Contains(filter) || p.Director.Contains(filter)).AsQueryable();
            if (year != 0) serials = serials.Where(p => p.YearOfCreateDate == year).AsQueryable();
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {

                    default:
                        break;
                }
            }
            int take = 3;
            int skip = (pageId - 1) * take;
            return Tuple.Create(await serials.Select(p => new GetSerialsInSiteDto
            {
                DateTime = p.CraeteDate,
                Director = p.Director,
                FirstCategory = p.SerialCategorySerials.FirstOrDefault().SerialCategory.Title,
                Title = p.Title,
                Id = p.Id,
                ImageName = p.Image,
                IsFree = p.IsFree,
                Year = p.YearOfCreateDate,
                AvvrageRate = p.SerialComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

            }).Skip(skip).Take(take).ToListAsync(), serials.Count() / take);
        }

        public async Task<string> GetSerialTitleById(long id)
        {
            return await _context.Serials.Where(p => p.Id == id).Select(p => p.Title).FirstOrDefaultAsync();
        }

        public async Task IncreaseSerialViewCount(long id)
        {
            var serial = await GetSerialById(id);
            if (serial.ViewCount == null) serial.ViewCount=1;
            else serial.ViewCount++;
            await UpdateSerial(serial);
        }

        public async Task<bool> IsFreeSerial(long id)
        {
            return await _context.Serials.Where(p => p.Id == id).Select(p => p.IsFree).FirstOrDefaultAsync();
        }

        public async Task<Tuple<List<ShowSerialCommentDto>, int>> ShowEpisodeCommentsInAdmin(long episodeId, int pageId = 1, string filter = "")
        {
            int take = 5;
            int skip = (pageId - 1) * take;
            var comments = _context.SerialEpisodeComments.Where(p => p.SerialEpisodeId == episodeId && p.ParentId == null).OrderByDescending(p => p.CraeteDate).AsQueryable();
            if (!string.IsNullOrEmpty(filter)) comments = comments.Where(p => p.Title.Contains(filter) || p.Text.Contains(filter)).AsQueryable();
            return Tuple.Create(await comments.Select(p => new ShowSerialCommentDto
            {
                CreateDate = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                Rate = p.Rate,
                Subject = p.Title,
                Text = p.Text,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                UserImage = _userService.GetUserImageByIdNoAsync(p.UserId),

            }).Skip(skip).Take(take).ToListAsync(), _context.SerialEpisodeComments.Where(p => p.SerialEpisodeId == episodeId).Count() / take);
        }

        public async Task<Tuple<List<ShowSerialCommentDto>, int>> ShowEpisodeComment(long episodeId, int pageId = 1)
        {
            int take = 5;
            int skip = (pageId - 1) * take;
            return Tuple.Create(await _context.SerialEpisodeComments.Where(p => p.SerialEpisodeId == episodeId && p.ParentId == null).OrderByDescending(p => p.CraeteDate).Select(p => new ShowSerialCommentDto
            {
                CreateDate = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                Rate = p.Rate,
                Subject = p.Title,
                Text = p.Text,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                UserImage = _userService.GetUserImageByIdNoAsync(p.UserId),


            }).Skip(skip).Take(take).ToListAsync(), _context.SerialEpisodeComments.Where(p => p.SerialEpisodeId == episodeId).Count() / take);
        }

        public async Task<List<ShowNearEpisodeDto>> ShowNearEpisode(long episodeId, long serialId)
        {
            var serialEpisodes = await _context.SerialEpisodes.Include(p=>p.SerialEpisodeComments).Where(p => p.SerialId == serialId).OrderByDescending(p => p.CraeteDate).ToListAsync();
            int episodeIndex = serialEpisodes.IndexOf(await GetEpisodeById(episodeId));
            int count = serialEpisodes.Count();
            var result = new List<SerialEpisode>();
            if (episodeIndex + 1 <= count - 1) result.Add(serialEpisodes[episodeIndex + 1]);
            if (episodeIndex + 2 <= count - 1) result.Add(serialEpisodes[episodeIndex + 2]);
            if (episodeIndex - 1 > -1) result.Add(serialEpisodes[episodeIndex - 1]);
            if (episodeIndex - 2 > -1) result.Add(serialEpisodes[episodeIndex - 2]);
            return result.Select(p => new ShowNearEpisodeDto
            {
                Id = p.Id,
                Title = p.Title,
                Image = p.Image,
                AvvrageRate = p.SerialEpisodeComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((p.SerialEpisodeComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.SerialEpisodeComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

            }).ToList();
        }

        public async Task<Tuple<List<ShowSerialCommentDto>, int>> ShowSerialComment(long serialId, int pageId = 1)
        {
            int take = 5;
            int skip = (pageId - 1) * take;
            return Tuple.Create(await _context.SerialComments.Where(p => p.SerialId == serialId && p.ParentId == null).OrderByDescending(p => p.CraeteDate).Select(p => new ShowSerialCommentDto
            {
                CreateDate = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                Rate = p.Rate,
                Subject = p.Title,
                Text = p.Text,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                UserImage = _userService.GetUserImageByIdNoAsync(p.UserId),


            }).Skip(skip).Take(take).ToListAsync(), _context.SerialComments.Where(p => p.SerialId == serialId).Count() / take);
        }

        public async Task<Tuple<List<ShowSerialCommentDto>,int>> ShowSerialCommentsInAdmin(long serialId, int pageId = 1, string filter = "")
        {
            int take = 5;
            int skip = (pageId - 1) * take;
            var comments = _context.SerialComments.Where(p => p.SerialId == serialId && p.ParentId == null).OrderByDescending(p => p.CraeteDate).AsQueryable();
            if (!string.IsNullOrEmpty(filter)) comments = comments.Where(p => p.Title.Contains(filter) || p.Text.Contains(filter)).AsQueryable();
            return Tuple.Create(await comments.Select(p => new ShowSerialCommentDto
            {
                CreateDate = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                Rate = p.Rate,
                Subject = p.Title,
                Text = p.Text,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                UserImage = _userService.GetUserImageByIdNoAsync(p.UserId),

            }).Skip(skip).Take(take).ToListAsync(), _context.SerialComments.Where(p => p.SerialId == serialId).Count() / take);
        }

        public async Task<ShowEpisodeInSiteDto> ShowSerialEpisode(long id)
        {
            return await _context.SerialEpisodes.Include(p=>p.SerialEpisodeComments).Include(p => p.SerialEpisodeFiles).Include(p => p.Serial).Where(p => p.Id == id).Select(p => new ShowEpisodeInSiteDto
            {
                Description = p.Description,
                ImageName = p.Image,
                SerialName = p.Serial.Title,
                Title = p.Title,
                Id = p.Id,
                AvvrageRate = p.SerialEpisodeComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((p.SerialEpisodeComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.SerialEpisodeComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)
,


                EpisodeFiles = p.SerialEpisodeFiles.Select(x => new ShowEpisodeFileInSiteDto
                {
                    FileName = x.FileName,
                    Quality = x.Quality
                }).ToList(),
                SerialId = p.SerialId
            }).FirstOrDefaultAsync();
        }

        public async Task<ShowSerialInSiteDto> ShowSerialInSite(long id)
        {
            var serialCategories = await _context.SerialCategorySerials.Include(p => p.SerialCategory).Where(p => p.SerialId == id).Select(p => p.SerialCategory.Title).ToListAsync();
            var serialSeasons = await _context.SerialSeasons.Where(p => p.SerialId == id).Select(p => new ShowSerialSeasonDto { SeasonId = p.Id, SeasonTitle = p.Title }).ToListAsync();
            var serialEpisodes = await _context.SerialEpisodes.Include(p=>p.SerialEpisodeComments).Where(p => p.SerialId == id).Select(p => new ShowSerialEpisodesDto
            {
                EpisodeId = p.Id,
                EpisodeImage = p.Image,
                EpisodeTitle = p.Title,
                SeasonId = p.SeasonId,
                AvvrageRate = p.SerialEpisodeComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((p.SerialEpisodeComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.SerialEpisodeComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

            }).ToListAsync();

            return await _context.Serials.Include(p=>p.SerialComments).Include(p => p.SerialEpisodes).Include(p => p.SerialSeasons).Where(p => p.Id == id).Select(p => new ShowSerialInSiteDto
            {
                Id = p.Id,
                AgeRestriction = p.AgeRestriction,
                Description = p.Description,
                ImageName = p.Image,
                Tiser = p.Tiser,
                Title = p.Title,
                Year = p.YearOfCreateDate,
                Categories = serialCategories,
                SerialSeasons = serialSeasons,
                SerialEpisodes = serialEpisodes,
                IsActive=p.IsActive,
                AvvrageRate = p.SerialComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

            }).FirstOrDefaultAsync();
        }

        public async Task<List<ShowSerialCommentDto>> ShowSubEpisodeComments(long episodeId)
        {
            return await _context.SerialEpisodeComments.Where(p => p.SerialEpisodeId == episodeId && p.ParentId != null).OrderByDescending(p => p.CraeteDate).Select(p => new ShowSerialCommentDto
            {
                CreateDate = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                Rate = p.Rate,
                Subject = p.Title,
                Text = p.Text,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                UserImage = _userService.GetUserImageByIdNoAsync(p.UserId),
                ParentId = p.ParentId,
                //  ParentText=GetCommentTextByIdNoAsync(p.ParentId)

            }).ToListAsync();
        }

        public async Task<List<ShowSerialCommentDto>> ShowSubSerialComments(long serialId)
        {
            return await _context.SerialComments.Where(p => p.SerialId == serialId && p.ParentId != null).OrderByDescending(p => p.CraeteDate).Select(p => new ShowSerialCommentDto
            {
                CreateDate = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                Rate = p.Rate,
                Subject = p.Title,
                Text = p.Text,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                UserImage = _userService.GetUserImageByIdNoAsync(p.UserId),
                ParentId = p.ParentId,
                //  ParentText=GetCommentTextByIdNoAsync(p.ParentId)

            }).ToListAsync();
        }

        public async Task<List<ShowSerialCommentDto>> ShowSubSerialCommentsInAdmin(long? parentId)
        { 
            return await _context.SerialComments.Where(p => p.ParentId == parentId).Select(p => new ShowSerialCommentDto
            {
                Id = p.Id,
                CreateDate = p.CraeteDate.ToShamsi(),
                Subject = p.Title,
                Text = p.Text,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                UserImage = _userService.GetUserImageByIdNoAsync(p.UserId),

            }).ToListAsync();
        }

        public async Task UpdateEpisdoe(SerialEpisode episode)
        {
            _context.SerialEpisodes.Update(episode);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateSeason(SerialSeason season)
        {
            _context.SerialSeasons.Update(season);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSerial(Serial serial)
        {
            _context.Serials.Update(serial);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSerialCategory(SerialCategory serialCategory)
        {
            _context.SerialCategories.Update(serialCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistEpisodeComment(long id)
        {
            return await _context.SerialEpisodeComments.AnyAsync(p=>p.Id==id);
        }

        public async Task<List<ShowSerialCommentDto>> ShowSubEpisodeCommentsInAdmin(long? parentId)
        {
            return await _context.SerialEpisodeComments.Where(p => p.ParentId == parentId).Select(p => new ShowSerialCommentDto
            {
                Id = p.Id,
                CreateDate = p.CraeteDate.ToShamsi(),
                Subject = p.Title,
                Text = p.Text,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                UserImage = _userService.GetUserImageByIdNoAsync(p.UserId),

            }).ToListAsync();
        }

        public async Task<bool> DeleteEpisodeComment(long id)
        {
            if (await ExistEpisodeComment(id))
            {
                var comment = await _context.SerialEpisodeComments.FindAsync(id);
                comment.IsRemoved = true;
                comment.RemoveDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<List<LastCommentInAdminPageDto>> GetLastSerialCommentIsnAdminPage(int take)
        {
            return await _context.SerialComments.Include(p=>p.Serial).OrderByDescending(p => p.CraeteDate).Take(take).Select(p => new LastCommentInAdminPageDto
            {
                MediaName = p.Serial.Title,
                Rate = p.Rate,
                Subject = p.Title,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                CreateDate = p.CraeteDate
            }).ToListAsync();
        }

        public async Task<List<LastCommentInAdminPageDto>> GetLastEpisodeCommentIsnAdminPage(int take)
        {
            return await _context.SerialEpisodeComments.Include(p=>p.SerialEpisode).OrderByDescending(p => p.CraeteDate).Take(take).Select(p => new LastCommentInAdminPageDto
            {
                MediaName = p.SerialEpisode.Title,
                Rate = p.Rate,
                Subject = p.Title,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                CreateDate = p.CraeteDate
            }).ToListAsync();
        }

        public async Task<List<BestMediaInAdminPageDto>> GetBestSerialsInAdminPage(int take)
        {
            return await _context.Serials.Include(p=>p.SerialComments).OrderByDescending(p => p.SerialComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
               ((p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.SerialComments
               .Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)).Take(take).Select(p => new BestMediaInAdminPageDto
               {
                   CreateDate = p.CraeteDate,
                   IsSerial = true,
                   Name = p.Title,
                   Rate = p.SerialComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                       ((p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.SerialComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

               }).ToListAsync();
             
        }
    }
}
