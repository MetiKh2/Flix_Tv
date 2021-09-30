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
using Flix_Tv.Domain.Entites.Enums;
using Flix_Tv.Domain.Entites.Serials;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.Services.Interfaces
{
    public interface ISerialService
    {
        #region SerialCategory
        Task<SerialCategory> CreateSerialCategory(string name, IFormFile image);
        Task<bool> DeleteSerialCategory(long id);
        Task<SerialCategory> GetSerialCategoryById(long id);
        Task UpdateSerialCategory(SerialCategory serialCategory);
        Task<Tuple<List<GetSerialCategoriesDto>, int, int>> GetSerialCategoriesInAdmin(int pageId = 1, string filter = "");
        Task EditSerialCategory(SerialCategoryForEditDto dto, IFormFile image);
        Task<List<GetSerialCategoriesForAdd_EditSerialsDto>> GetSerialCategories();
        Task<List<GetMediaCategoriesDto>> GetSerialCategoriesForSite();
        Task<bool> ExistsSerialCategory(long id);
        Task<string> GetSerialCategoryTitleById(long id);
        string GetSerialCategoryTitleByIdNoAsync(long id);
        #endregion

        #region Serial
        Task<long> CreateSerial(CreateSerialDto dto);
        Task<Tuple<List<GetSerialsInAdminDto>, int, int>> GetSerialsInAdmin(int pageId = 1, string filter = "", string sort = "");
        Task<Serial> GetSerialById(long id);
        Task UpdateSerial(Serial serial);
        Task<bool> DeleteSerial(long id);
        Task EditSerial(EditSerialDto dto);
        Task<bool> ExistsSerial(long serialId);
        Task<string> GetSerialTitleById(long id);
        Task<List<GetIsSliderMediaDto>> GetIsSliderSerials();
        Task<List<GetLastMediasDto>> GetLastSerials(int take);
        Task<Tuple<List<GetSerialsInSiteDto>, int>> GetSerialsInSite(int pageId = 1, string filter = "", long categoryId = 0, long year = 0, string sort = "date");
        Task<ShowSerialInSiteDto> ShowSerialInSite(long id);
        Task<List<GetFavoriteMediasDto>> GetFavoriteSerials(long userId);
        //Task<long> GetSerialIdByEpisodeId(long id);
        Task<bool> IsFreeSerial(long id);
        Task<List<GetSerialsInSiteDto>> GetMaybeLikeSerials(long serialId);
        Task<List<Serial>> GetSerialsBySerialCategoryId(long categoryId);
        Task<bool> GetIsActiveSerial(long id);
        Task IncreaseSerialViewCount(long id);
        Task<List<BestMediaInAdminPageDto>> GetBestSerialsInAdminPage(int take);
        #endregion

        #region SerialCategorySerial
        Task CreateSerialCategorySerials(long serialId, List<long> selectedSerialCategories);
        Task<List<long>> GetSerialCategoryIdSerials(long serialId);
        Task EditSerialCategorySerials(long serialId, List<long> selectedSerialCategories);
        Task DeleteSerialCategorySerial(long serialId);
        #endregion


        #region Seasons
        Task CreateSeason(CreateSerialSeasonDto dto);
        Task<List<GetSerialSeasonsInAdminDto>> GetSerialSeasonsInAdmin(long serialId);
        Task<bool> DeleteSeason(long id);
        Task<SerialSeason> GetSerialSeasonById(long id);
        Task UpdateSeason(SerialSeason season);
        Task EditSeason(EditSerialSeasonDto dto);
        Task<long> GetSerialIdBySeasonId(long seasonId);
        Task<bool> ExsistSeason(long id);
        Task<string> GetSeasonNameById(long id);
        #endregion

        #region Episode
        Task CreateEpisode(CreateSerialEpisodeDto dto);
        Task<List<GetSerialEpisodesInAdminDto>> GetSerialEpisodesInAdmin(long seasonId);
        Task<bool> DeleteEpisode(long id);
        Task<SerialEpisode> GetSerialEpisodeById(long Id);
        Task UpdateEpisdoe(SerialEpisode episode);
        Task EditEpisode(EditSerialEpisodeDto dto);
        Task<bool> ExistsEpisode(long id);
        Task<long> GetSerialIdByEpisodeId(long episodeId);
        Task<ShowEpisodeInSiteDto> ShowSerialEpisode(long id);
        Task<long> GetEpisodeIdByFileName(string fileName);
        Task<SerialEpisode> GetEpisodeById(long Id);
        Task<List<ShowNearEpisodeDto>> ShowNearEpisode(long episodeId,long serialId);
        #endregion

        #region File
        Task<string> CreateEpisodeFile(long episodeId, IFormFile episodeFile, Quality quality);
        Task<string> GetEpisodeFileSrcByEpisodeIdAndQuality(long episodeId, Quality quality);
        #endregion

        #region SerialComment
        Task<Tuple<List<ShowSerialCommentDto>, int>> ShowSerialComment(long serialId, int pageId = 1);
        Task<ShowSerialCommentDto> CreateSerialComment(string text, string subject, short? rate, string userName, long serialId, long? parentId);
        Task<List<ShowSerialCommentDto>> ShowSubSerialComments(long serialId);
        Task<bool> ExistComment(long id);
        Task<string> GetCommentTextById(long id);
        Task<bool> ExistsUserSerialVote(long serialId, string username);
        Task<List<GetLastUserCommentsDto>> GetLastUserSerialComments(long userId, int take);
        Task<Tuple<List<ShowSerialCommentDto>,int>> ShowSerialCommentsInAdmin(long serialId, int pageId = 1, string filter = "");
        Task<List<ShowSerialCommentDto>> ShowSubSerialCommentsInAdmin(long? parentId);
        Task<bool> DeleteSerialComment(long id);
        Task<List<LastCommentInAdminPageDto>> GetLastSerialCommentIsnAdminPage(int take);
        #endregion

        #region EpisodeComment
        Task<bool> ExistsUserEpisodeVote(long episodeId, long userId);
        Task<ShowSerialCommentDto> CreateEpisodeComment(string text, string subject, short? rate, long userId, long episodeId, long? parentId);
        Task<Tuple<List<ShowSerialCommentDto>, int>> ShowEpisodeComment(long episodeId, int pageId = 1);
        Task<List<ShowSerialCommentDto>> ShowSubEpisodeComments(long episodeId);
        Task<string> GetEpisodeCommentTextById(long id);
        Task<List<GetLastUserCommentsDto>> GetLastUserEpisodeComments(long userId,int take);
        Task<Tuple<List<ShowSerialCommentDto>, int>> ShowEpisodeCommentsInAdmin(long episodeId, int pageId = 1, string filter = "");
        Task<bool> ExistEpisodeComment(long id);
        Task<List<ShowSerialCommentDto>> ShowSubEpisodeCommentsInAdmin(long? parentId);
        Task<bool> DeleteEpisodeComment(long id);
        Task<List<LastCommentInAdminPageDto>> GetLastEpisodeCommentIsnAdminPage(int take);
        #endregion

        #region UserFavoriteSerial
        Task<bool> ExistsUserFavoriteSerial(long userId, long serialId);
        Task CreateUserFavoriteSerial(long userId, long serialId);
        Task DeleteUserFavoriteSerial(long userId, long serialId);
        #endregion
    }
}
