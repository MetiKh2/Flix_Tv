using Flix_Tv.Application.DTOs.AdminPage;
using Flix_Tv.Application.DTOs.Movie;
using Flix_Tv.Application.DTOs.Movie.Admin;
using Flix_Tv.Application.DTOs.Movie.Site;
using Flix_Tv.Application.DTOs.MovieCategory.Admin;
using Flix_Tv.Application.DTOs.MovieCategory.Admin.Edit;
using Flix_Tv.Application.DTOs.MovieCategory.Admin.Movies;
using Flix_Tv.Application.DTOs.MovieComment.Site;
using Flix_Tv.Application.DTOs.Public;
using Flix_Tv.Application.DTOs.User.Profile;
using Flix_Tv.Domain.Entites.Enums;
using Flix_Tv.Domain.Entites.Movies;
using Flix_Tv.Domain.Entites.Serials;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Application.Services.Interfaces
{
    public interface IMovieService
    {

        #region MovieCategory
        Task<MovieCategory> CreateMovieCategory(string name, IFormFile image);
        Task<Tuple<List<GetMovieCategoriesDto>, int,int>> GetMovieCategoriesInAdmin(int pageId = 1, string filter = "");
        Task<List<GetMovieCategoriesForAdd_EditMoviesDto>> GetMovieCategoriesInAdmin();
        Task<bool> DeleteMovieCategory(long id);
        Task<MovieCategory> GetMovieCategoryById(long id);
        Task UpdateMovieCategory(MovieCategory movieCategory);
        Task EditMovieCategory(MovieCategoryForEditDto dto, IFormFile image);
        Task<List<GetMediaCategoriesDto>> GetMovieCategoriesForSite();
        Task<bool> ExistsMovieCategory(long id);
        Task<string> GetMovieCategoryTitleById(long id);
        string GetMovieCategoryTitleByIdNoAsync(long id);
        Task<int> GetMovieCategoryMoviesCount(long movieId);
       // Task<List<long>> GetMovieCategoriesIdByMovieId(long movieId);
        #endregion

        #region Movie
        Task<long> CreateMovie(CreateMovieDto dto);
        Task<Tuple<List<GetMoviesInAdminDto>, int, int>> GetMoviesInAdmin(int pageId=1,string sort="",string filter="");
        Task<bool> DeleteMovie(long id);
        Task<Movie> GetMovieById(long id);
        Task UpdateMovie(Movie movie);
        Task EditMovie(EditMovieDto dto);
        Task<bool> MovieExists(long id);

        Task<List<GetLastMediasDto>> GetLastMovies(int take);

        Task<List<GetIsSliderMediaDto>> GetIsSliderMovies();
        Task<Tuple<List<GetMoviesInSiteDto>, int>> GetMoviesInSite(int pageId = 1, string filter = "", long categoryId = 0, long year = 0, string sort = "date");
        Task<ShowMovieInSiteDto> ShowMovieInSite(long id);

        Task<List<GetFavoriteMediasDto>> GetFavoriteMovies(long userId);
        Task<List<GetMoviesInSiteDto>> GetMaybeLikeMovies(long movieId);
        Task<List<Movie>> GetMoviesByMovieCategoryId(long categoryId);
        Task<int> GetMovieAvrageRateByMovieId(long id); 
        double GetMovieAvrageRateByMovieIdNoAsync(long id);
        Task IncreaseMovieViewCount(long id);
        Task<List<BestMediaInAdminPageDto>> GetBestMoviesInAdminPage(int take);
        #endregion


        #region MovieCategoryMovie
        Task CreateMovieCategoryMovies(long movieId, List<long> selectedMovieCategories);
        Task DeleteMovieCategoryMovies(long movieId);
        Task EditMovieCategoryMovies(long movieId, List<long> selectedMovieCategories);
        Task<List<long>> GetMovieCategoryIdMoviesIdByMovieId(long movieId);

        #endregion

        #region Files
        Task<string> GetMovieTitleById(long id);
        Task<string> CreateMovieFile(long movieId,IFormFile movieFile,Quality quality);
        Task<string> GetMovieFileSrcByMovieIdAndQuality(long movieId,Quality quality);
        Task<long> GetMovieIdByFileName(string fileName);
        #endregion

        #region MovieComment
        Task<ShowMovieCommentDto> CreateMovieComment(string text, string subject, short? rate,string userName,long movieId,long? parentId);
        Task<Tuple<List<ShowMovieCommentDto>, int>> ShowMovieComment(long movieId,int pageId=1);
        Task<bool> ExistComment(long id);
        Task<string> GetCommentTextById(long? id);
        string GetCommentTextByIdNoAsync(long? id);
        Task<List<ShowMovieCommentDto>> ShowSubMovieComments(long movieId);
        Task<bool> ExistsUserMovieVote(string username,long movieId);
        Task<List<GetLastUserCommentsDto>> GetLastUserMovieComments(long userId,int take);
        Task<Tuple<List<ShowMovieCommentDto>, int>> ShowMovieCommentsInAdmin(long movieId, int pageId = 1,string filter="");
        Task<List<ShowMovieCommentDto>> ShowSubMovieCommentsInAdmin(long? parentId);
        Task<bool> DeleteMovieComment(long id);
        Task<List<LastCommentInAdminPageDto>> GetLastMovieCommentInAdminPage(int take);
        #endregion

        #region UserFavoriteMovie
        Task<bool> ExistsUserFavoriteMovie(long userId,long movieId);
        Task CreateUserFavoriteMovie(long userId,long movieId);
        Task DeleteUserFavoriteMovie(long userId,long movieId);
        #endregion
    }
}
