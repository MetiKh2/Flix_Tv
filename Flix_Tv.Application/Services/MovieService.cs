using Flix_Tv.Application.DatabaseInterfaces;
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
using Flix_Tv.Application.Services.Interfaces;
using Flix_Tv.Common.Convertors;
using Flix_Tv.Common.Generators;
using Flix_Tv.Common.Security;
using Flix_Tv.Domain.Entites.Enums;
using Flix_Tv.Domain.Entites.Movies;
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
    public class MovieService : IMovieService
    {
        private readonly IUserService _userService;
        private readonly IFlixTvContext _context;
        public MovieService(IFlixTvContext context, IUserService userService)
        {
            _userService = userService;
            _context = context;
        }

        public async Task<long> CreateMovie(CreateMovieDto dto)
        {
            var movie = new Movie
            {
                IsFree = dto.IsFree,
                Time = new TimeSpan(dto.Hours, dto.Minutes, dto.Seconds),
                AgeRestriction = dto.AgeRestriction,
                CraeteDate = DateTime.Now,
                Description = dto.Description,
                Tag = dto.Tag,
                Title = dto.Title,
                YearOfCreateDate = dto.YearOfCreateDate,
                Director = dto.Director
            };
            if (dto.ImageFile != null && dto.ImageFile.IsImage())
            {
                movie.ImageSrc = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.ImageFile.FileName);
                SaveFileInServer saveFile = new SaveFileInServer();
                var pathName = await saveFile.SaveFile("wwwroot/Movies/Images", movie.ImageSrc, dto.ImageFile);
                ImageResizer resizer = new ImageResizer();
                string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/ThumbImages", movie.ImageSrc);
                resizer.Image_resize(pathName, resizeImageName, 350);
            }
            if (dto.TiserFile != null && dto.TiserFile.IsMovie())
            {
                movie.TiserFile = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.TiserFile.FileName);
                SaveFileInServer saveFile = new SaveFileInServer();
                await saveFile.SaveFile("wwwroot/Movies/Tisers", movie.TiserFile, dto.TiserFile);
            }
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie.Id;
        }

        public async Task<MovieCategory> CreateMovieCategory(string name, IFormFile image)
        {
            var movieCategory = new MovieCategory
            {
                CraeteDate = DateTime.Now,
                Title = name
            };
            if (image != null && image.IsImage())
            {
                movieCategory.ImageName = GeneratCode.GenerateUniqCode() + Path.GetExtension(image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Categories/Images", movieCategory.ImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                ImageResizer resizer = new ImageResizer();
                string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Categories/ThumbImages", movieCategory.ImageName);
                resizer.Image_resize(imagePath, resizeImageName, 350);
            }
            await _context.MovieCategories.AddAsync(movieCategory);
            await _context.SaveChangesAsync();
            return movieCategory;
        }

        public async Task CreateMovieCategoryMovies(long movieId, List<long> selectedMovieCategories)
        {
            foreach (var item in selectedMovieCategories)
            {
                await _context.MovieCategoryMovies.AddAsync(new MovieCategoryMovie
                {
                    CategoryId = item,
                    CraeteDate = DateTime.Now,
                    MovieId = movieId,
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task<ShowMovieCommentDto> CreateMovieComment(string text, string subject, short? rate, string userName, long movieId, long? parentId)
        {
            var userId = await _userService.GetUserIdByUserName(userName);
            var comment = new MovieComment
            {
                CraeteDate = DateTime.Now,
                MovieId = movieId,
                Rate = rate,
                Text = text,
                Title = subject,
                UserId = userId
           ,
                ParentId = parentId
            };

            if (rate == null) rate = 0;
            await _context.MovieComments.AddAsync(comment);
            await _context.SaveChangesAsync();
            var result =
             new ShowMovieCommentDto
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

        public async Task<string> CreateMovieFile(long movieId, IFormFile movieFile, Quality quality)
        {
            if (movieFile.FileName.ToLower().EndsWith(".mp4") == false) return null;
            var newmovieFile = new MovieFile
            {
                CraeteDate = DateTime.Now,
                MovieId = movieId,
                Quality = quality,
                FileName = GeneratCode.GenerateUniqCode() + quality + Path.GetExtension(movieFile.FileName),
            };
            string pathName = null;
            SaveFileInServer saveFile = new SaveFileInServer();
            switch (quality)
            {
                case Quality.Q240:
                    if (await _context.MovieFiles.AnyAsync(p => p.MovieId == movieId && p.Quality == Quality.Q240))
                    {
                        var lastMovieFile = await _context.MovieFiles.Where(p => p.MovieId == movieId && p.Quality == Quality.Q240).FirstOrDefaultAsync();
                        var lastMovieFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Files/240p", lastMovieFile.FileName);
                        if (File.Exists(lastMovieFilePath)) File.Delete(lastMovieFilePath);
                        lastMovieFile.IsRemoved = true;
                        lastMovieFile.RemoveDate = DateTime.Now;
                    }
                    pathName = await saveFile.SaveFile("wwwroot/Movies/Files/240p", newmovieFile.FileName, movieFile);
                    break;
                case Quality.Q360:
                    if (await _context.MovieFiles.AnyAsync(p => p.MovieId == movieId && p.Quality == Quality.Q360))
                    {
                        var lastMovieFile = await _context.MovieFiles.Where(p => p.MovieId == movieId && p.Quality == Quality.Q360).FirstOrDefaultAsync();
                        var lastMovieFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Files/360p", lastMovieFile.FileName);
                        if (File.Exists(lastMovieFilePath)) File.Delete(lastMovieFilePath);
                        lastMovieFile.IsRemoved = true;
                        lastMovieFile.RemoveDate = DateTime.Now;
                    }
                    pathName = await saveFile.SaveFile("wwwroot/Movies/Files/360p", newmovieFile.FileName, movieFile);
                    break;
                case Quality.Q480:
                    if (await _context.MovieFiles.AnyAsync(p => p.MovieId == movieId && p.Quality == Quality.Q480))
                    {
                        var lastMovieFile = await _context.MovieFiles.Where(p => p.MovieId == movieId && p.Quality == Quality.Q480).FirstOrDefaultAsync();
                        var lastMovieFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Files/480p", lastMovieFile.FileName);
                        if (File.Exists(lastMovieFilePath)) File.Delete(lastMovieFilePath);
                        lastMovieFile.IsRemoved = true;
                        lastMovieFile.RemoveDate = DateTime.Now;
                    }
                    pathName = await saveFile.SaveFile("wwwroot/Movies/Files/480p", newmovieFile.FileName, movieFile);
                    break;
                case Quality.Q720:
                    if (await _context.MovieFiles.AnyAsync(p => p.MovieId == movieId && p.Quality == Quality.Q720))
                    {
                        var lastMovieFile = await _context.MovieFiles.Where(p => p.MovieId == movieId && p.Quality == Quality.Q720).FirstOrDefaultAsync();
                        var lastMovieFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Files/720p", lastMovieFile.FileName);
                        if (File.Exists(lastMovieFilePath)) File.Delete(lastMovieFilePath);
                        lastMovieFile.IsRemoved = true;
                        lastMovieFile.RemoveDate = DateTime.Now;
                    }
                    pathName = await saveFile.SaveFile("wwwroot/Movies/Files/720p", newmovieFile.FileName, movieFile);
                    break;
                case Quality.Q1080:
                    if (await _context.MovieFiles.AnyAsync(p => p.MovieId == movieId && p.Quality == Quality.Q1080))
                    {
                        var lastMovieFile = await _context.MovieFiles.Where(p => p.MovieId == movieId && p.Quality == Quality.Q1080).FirstOrDefaultAsync();
                        var lastMovieFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Files/1080p", lastMovieFile.FileName);
                        if (File.Exists(lastMovieFilePath)) File.Delete(lastMovieFilePath);
                        lastMovieFile.IsRemoved = true;
                        lastMovieFile.RemoveDate = DateTime.Now;
                    }
                    pathName = await saveFile.SaveFile("wwwroot/Movies/Files/1080p", newmovieFile.FileName, movieFile);
                    break;
                case Quality.Q4k:
                    if (await _context.MovieFiles.AnyAsync(p => p.MovieId == movieId && p.Quality == Quality.Q4k))
                    {
                        var lastMovieFile = await _context.MovieFiles.Where(p => p.MovieId == movieId && p.Quality == Quality.Q4k).FirstOrDefaultAsync();
                        var lastMovieFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Files/4k", lastMovieFile.FileName);
                        if (File.Exists(lastMovieFilePath)) File.Delete(lastMovieFilePath);
                        lastMovieFile.IsRemoved = true;
                        lastMovieFile.RemoveDate = DateTime.Now;
                    }
                    pathName = await saveFile.SaveFile("wwwroot/Movies/Files/4k", newmovieFile.FileName, movieFile);
                    break;

            }

            await _context.MovieFiles.AddAsync(newmovieFile);
            await _context.SaveChangesAsync();
            return pathName;
        }

        public async Task CreateUserFavoriteMovie(long userId, long movieId)
        {
            var userFavoriteMovie = new UserFavoriteMovie
            {
                UserId = userId,
                CraeteDate = DateTime.Now,
                MovieId = movieId,
            };
            await _context.UserFavoriteMovies.AddAsync(userFavoriteMovie);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteMovie(long id)
        {
            try
            {
                var movie = await GetMovieById(id);
                if (movie != null)
                {
                    movie.IsRemoved = true;
                    movie.RemoveDate = DateTime.Now;
                    await DeleteMovieCategoryMovies(id);
                    await UpdateMovie(movie);
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

        public async Task<bool> DeleteMovieCategory(long id)
        {
            try
            {
                var category = await GetMovieCategoryById(id);
                if (category != null)
                {
                    category.IsRemoved = true;
                    category.RemoveDate = DateTime.Now;
                    await UpdateMovieCategory(category);
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

        public async Task DeleteMovieCategoryMovies(long movieId)
        {
            var movieCategoriesMovie = await _context.MovieCategoryMovies.Where(p => p.MovieId == movieId).ToListAsync();
            foreach (var item in movieCategoriesMovie)
            {
                item.IsRemoved = true;
                item.RemoveDate = DateTime.Now;
            }
        }

        public async Task<bool> DeleteMovieComment(long id)
        {
            if (await ExistComment(id))
            {
                var comment = await _context.MovieComments.FindAsync(id);
                comment.IsRemoved = true;
                comment.RemoveDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task DeleteUserFavoriteMovie(long userId, long movieId)
        {
            var userFavoriteMovie = await _context.UserFavoriteMovies.Where(p => p.MovieId == movieId && p.UserId == userId).FirstOrDefaultAsync();
            userFavoriteMovie.IsRemoved = true;
            userFavoriteMovie.RemoveDate = DateTime.Now;
            _context.UserFavoriteMovies.Update(userFavoriteMovie);
            await _context.SaveChangesAsync();
        }

        public async Task EditMovie(EditMovieDto dto)
        {
            var movie = await GetMovieById(dto.Id);
            if (movie != null)
            {
                movie.AgeRestriction = dto.AgeRestriction;
                movie.Title = dto.Title;
                movie.Time = new TimeSpan(dto.Hours, dto.Minutes, dto.Seconds);
                movie.Description = dto.Description;
                movie.IsFree = dto.IsFree;
                movie.Tag = dto.Tag;
                movie.UpdateDate = DateTime.Now;
                movie.YearOfCreateDate = dto.YearOfCreateDate;
                movie.Director = dto.Director;
                if (dto.ImageFile != null && dto.ImageFile.IsImage())
                {
                    if (!string.IsNullOrEmpty(movie.ImageSrc))
                    {
                        var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Images", movie.ImageSrc);
                        var deleteThumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/ThumbImages", movie.ImageSrc);
                        if (File.Exists(deleteImagePath)) File.Delete(deleteImagePath);
                        if (File.Exists(deleteThumbImagePath)) File.Delete(deleteThumbImagePath);
                    }
                    movie.ImageSrc = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.ImageFile.FileName);
                    var saveFile = new SaveFileInServer();
                    var imagePath = await saveFile.SaveFile("wwwroot/Movies/Images", movie.ImageSrc, dto.ImageFile);
                    ImageResizer resizer = new ImageResizer();
                    string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/ThumbImages", movie.ImageSrc);
                    resizer.Image_resize(imagePath, resizeImageName, 350);
                }
                if (dto.TiserFile != null && dto.TiserFile.IsMovie())
                {
                    if (!string.IsNullOrEmpty(movie.TiserFile))
                    {
                        var deleteTiserPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Tisers", movie.TiserFile);
                        if (File.Exists(deleteTiserPath)) File.Delete(deleteTiserPath);
                    }
                    movie.TiserFile = GeneratCode.GenerateUniqCode() + Path.GetExtension(dto.TiserFile.FileName);
                    var saveFile = new SaveFileInServer();
                    var tiserPath = await saveFile.SaveFile("wwwroot/Movies/Tisers", movie.TiserFile, dto.TiserFile);
                }
                await UpdateMovie(movie);
                //1
            }

        }

        public async Task EditMovieCategory(MovieCategoryForEditDto dto, IFormFile image)
        {
            var category = await GetMovieCategoryById(dto.Id);
            category.Title = dto.Title;
            if (image != null && image.IsImage())
            {
                if (!string.IsNullOrEmpty(category.ImageName))
                {
                    var deleteImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Categories/Images", category.ImageName);
                    var deleteThumbImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Categories/ThumbImages", category.ImageName);
                    if (File.Exists(deleteImagePath)) File.Delete(deleteImagePath);
                    if (File.Exists(deleteThumbImagePath)) File.Delete(deleteThumbImagePath);

                }
                category.ImageName = GeneratCode.GenerateUniqCode() + Path.GetExtension(image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Categories/Images", category.ImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                ImageResizer resizer = new ImageResizer();
                string resizeImageName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Movies/Categories/ThumbImages", category.ImageName);
                resizer.Image_resize(imagePath, resizeImageName, 350);
            }
            await UpdateMovieCategory(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditMovieCategoryMovies(long movieId, List<long> selectedMovieCategories)
        {
            await DeleteMovieCategoryMovies(movieId);
            await CreateMovieCategoryMovies(movieId, selectedMovieCategories);
        }

        public async Task<bool> ExistComment(long id)
        {
            return await _context.MovieComments.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> ExistsMovieCategory(long id)
        {
            return await _context.MovieCategories.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> ExistsUserFavoriteMovie(long userId, long movieId)
        {
            return await _context.UserFavoriteMovies.AnyAsync(p => p.UserId == userId && p.MovieId == movieId);
        }

        public async Task<bool> ExistsUserMovieVote(string username, long movieId)
        {
            var userId = await _userService.GetUserIdByUserName(username);
            return await _context.MovieComments.AnyAsync(p => p.MovieId == movieId && p.UserId == userId && p.Rate != null);
        }

        public async Task<List<BestMediaInAdminPageDto>> GetBestMoviesInAdminPage(int take)
        {
            return await _context.Movies.Include(p=>p.MovieComments).OrderByDescending(p => p.MovieComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
            ((p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.MovieComments
            .Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)).Take(take).Select(p => new BestMediaInAdminPageDto
            {
                CreateDate = p.CraeteDate,
                IsSerial = false,
                Name = p.Title,
                Rate = p.MovieComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

            }).ToListAsync();
        }


        public async Task<string> GetCommentTextById(long? id)
        {
            if (id == null) return "";
            return await _context.MovieComments.Where(p => p.Id == id.Value).Select(p => p.Text).FirstOrDefaultAsync();
        }

        public string GetCommentTextByIdNoAsync(long? id)
        {
            if (id == null) return "";
            return _context.MovieComments.Where(p => p.Id == id.Value).Select(p => p.Text).FirstOrDefault();

        }

        public async Task<List<GetFavoriteMediasDto>> GetFavoriteMovies(long userId)
        {
            return await _context.UserFavoriteMovies.Include(p => p.Movie).ThenInclude(p => p.MovieCategories).ThenInclude(p => p.Category).Where(p => p.UserId == userId).OrderByDescending(p => p.CraeteDate).Select(p => new GetFavoriteMediasDto
            {
                Id = p.Movie.Id,
                FirstCategory = p.Movie.MovieCategories.FirstOrDefault().Category.Title,
                Title = p.Movie.Title,
                Image = p.Movie.ImageSrc,
                IsFree = p.Movie.IsFree,
                IsSerial = false,
                Year = p.Movie.YearOfCreateDate
            }).ToListAsync();

        }

        public async Task<List<GetIsSliderMediaDto>> GetIsSliderMovies()
        {
            return await _context.Movies.Include(p => p.MovieComments).Include(p => p.MovieCategories).ThenInclude(p => p.Category).Where(p => p.IsSlider && p.IsActive).OrderBy(p => p.CraeteDate).Select(p => new GetIsSliderMediaDto
            {
                Id = p.Id,
                FirstCategory = p.MovieCategories.FirstOrDefault().Category.Title,
                Title = p.Title,
                IsFree = p.IsFree,
                YearOfCreate = p.YearOfCreateDate,
                ImageName = p.ImageSrc,
                IsSerial = false,
                AvvrageRate = p.MovieComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                      ((p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

            }).ToListAsync();
        }

        public async Task<List<LastCommentInAdminPageDto>> GetLastMovieCommentInAdminPage(int take)
        {
            return await _context.MovieComments.Include(p => p.Movie).OrderByDescending(p => p.CraeteDate).Take(take).Select(p => new LastCommentInAdminPageDto
            {
                MediaName = p.Movie.Title,
                Rate = p.Rate,
                Subject = p.Title,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                CreateDate = p.CraeteDate
            }).ToListAsync();
        }

        public async Task<List<GetLastMediasDto>> GetLastMovies(int take)
        {
            return await _context.Movies.Include(p => p.MovieCategories).ThenInclude(p => p.Category).Include(p => p.MovieComments).Where(p => p.IsActive).OrderByDescending(p => p.CraeteDate).Select(p => new GetLastMediasDto
            {
                Id = p.Id,
                FirstCategory = p.MovieCategories.FirstOrDefault().Category.Title,
                Title = p.Title,
                IsFree = p.IsFree,
                Year = p.YearOfCreateDate,
                ImageName = p.ImageSrc,
                IsSerial = false,
                DateTime = p.CraeteDate,
                Director = p.Director,
                AvvrageRate = p.MovieComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                      ((p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)


            }).Take(take).ToListAsync();
        }

        public async Task<List<GetLastUserCommentsDto>> GetLastUserMovieComments(long userId, int take)
        {
            return await _context.MovieComments.Include(p => p.Movie).Where(p => p.UserId == userId).OrderByDescending(p => p.CraeteDate).Take(take).Select(p => new GetLastUserCommentsDto
            {
                Id = p.Id,
                MediaTitle = p.Movie.Title,
                rate = p.Rate,
                Subject = p.Title,
                CreateDate = p.CraeteDate
            }).ToListAsync();
        }

        public async Task<List<GetMoviesInSiteDto>> GetMaybeLikeMovies(long movieId)
        {
            var movieCategriesId = await GetMovieCategoryIdMoviesIdByMovieId(movieId);
            var result = new List<GetMoviesInSiteDto>();
            foreach (var item in movieCategriesId)
            {
                var movies = await GetMoviesByMovieCategoryId(item);
                foreach (var movie in movies)
                {
                    if (!result.Any(p => p.Id == movie.Id) && movie.IsActive)
                    {
                        result.Add(new GetMoviesInSiteDto
                        {
                            Id = movie.Id,
                            DateTime = movie.CraeteDate,
                            FirstCategory = GetMovieCategoryTitleByIdNoAsync(item),
                            ImageName = movie.ImageSrc,
                            IsFree = movie.IsFree,
                            Title = movie.Title,
                            Year = movie.YearOfCreateDate,
                            AvvrageRate = movie.MovieComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((movie.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? movie.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

                        });
                    }

                }

            }
            return result;


        }

        public async Task<int> GetMovieAvrageRateByMovieId(long id)
        {
            int sumRate = await _context.MovieComments.Where(p => p.MovieId == id && p.Rate != null && p.Rate > 0 && p.Rate <= 10).SumAsync(p => p.Rate.Value);
            int rateCount = await _context.MovieComments.Where(p => p.MovieId == id && p.Rate != null && p.Rate > 0 && p.Rate <= 10).CountAsync();
            return sumRate / rateCount;
        }

        public double GetMovieAvrageRateByMovieIdNoAsync(long id)
        {
            int sumRate = _context.MovieComments.Where(p => p.MovieId == id && p.Rate != null && p.Rate > 0 && p.Rate <= 10).Sum(p => p.Rate.Value);
            int rateCount = _context.MovieComments.Where(p => p.MovieId == id && p.Rate != null && p.Rate > 0 && p.Rate <= 10).Count();
            return sumRate / rateCount;
        }

        public async Task<Movie> GetMovieById(long id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<List<GetMediaCategoriesDto>> GetMovieCategoriesForSite()
        {
            return await _context.MovieCategories.Include(p => p.MovieCategories).Where(p => p.ImageName != null).Select(p => new GetMediaCategoriesDto
            {
                Id = p.Id,
                Image = p.ImageName,
                IsSerial = false,
                MediaCount = p.MovieCategories.Where(x => x.Movie.IsActive).Count(),
                Title = p.Title
            }).ToListAsync();
        }



        public async Task<Tuple<List<GetMovieCategoriesDto>, int, int>> GetMovieCategoriesInAdmin(int pageId = 1, string filter = "")
        {
            var movieCategories = _context.MovieCategories.OrderByDescending(p => p.Id).AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                movieCategories = movieCategories.Where(p => p.Title.Contains(filter)).AsQueryable();
            }
            int take = 3;
            int skip = (pageId - 1) * take;
            return Tuple.Create(await movieCategories.Select(p => new GetMovieCategoriesDto
            {
                DateTime = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                Image = p.ImageName,
                Title = p.Title
            }).Skip(skip).Take(take).ToListAsync(), movieCategories.Count() / take, movieCategories.Count());
        }

        public async Task<List<GetMovieCategoriesForAdd_EditMoviesDto>> GetMovieCategoriesInAdmin()
        {
            return await _context.MovieCategories.OrderBy(p => p.Title).Select(p => new GetMovieCategoriesForAdd_EditMoviesDto
            {
                Id = p.Id,
                Title = p.Title
            }).ToListAsync();
        }

        public async Task<MovieCategory> GetMovieCategoryById(long id)
        {
            return await _context.MovieCategories.FindAsync(id);
        }

        public async Task<List<long>> GetMovieCategoryIdMoviesIdByMovieId(long movieId)
        {
            return await _context.MovieCategoryMovies.Where(p => p.MovieId == movieId).Select(p => p.CategoryId).ToListAsync();
        }

        public async Task<int> GetMovieCategoryMoviesCount(long movieId)
        {
            return await _context.MovieCategoryMovies.Where(p => p.MovieId == movieId).CountAsync();
        }

        public async Task<string> GetMovieCategoryTitleById(long id)
        {
            return await _context.MovieCategories.Where(p => p.Id == id).Select(p => p.Title).FirstOrDefaultAsync();
        }

        public string GetMovieCategoryTitleByIdNoAsync(long id)
        {
            return _context.MovieCategories.Where(p => p.Id == id).Select(p => p.Title).FirstOrDefault();
        }

        public async Task<string> GetMovieFileSrcByMovieIdAndQuality(long movieId, Quality quality)
        {
            return await _context.MovieFiles.Where(p => p.MovieId == movieId && p.Quality == quality).Select(p => p.FileName).FirstOrDefaultAsync();
        }

        public async Task<long> GetMovieIdByFileName(string fileName)
        {
            return await _context.MovieFiles.Where(p => p.FileName == fileName).Select(p => p.MovieId).FirstOrDefaultAsync();
        }

        public async Task<List<Movie>> GetMoviesByMovieCategoryId(long categoryId)
        {
            return await _context.MovieCategoryMovies.Include(p => p.Movie).ThenInclude(p => p.MovieComments).Where(p => p.CategoryId == categoryId).Select(p => p.Movie).ToListAsync();
        }

        public async Task<Tuple<List<GetMoviesInAdminDto>, int, int>> GetMoviesInAdmin(int pageId = 1, string sort = "", string filter = "")
        {
            var movies = _context.Movies.Include(p => p.MovieComments).OrderByDescending(p => p.CraeteDate).AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                movies = movies.Where(p => p.Title.Contains(filter)).AsQueryable();
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
                            movies = movies.OrderByDescending(p => p.ViewCount).AsQueryable();
                            break;
                        }
                    case "rate":
                        {
                            movies = movies.OrderByDescending(p => p.MovieComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) / ((p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)).AsQueryable();
                            break;
                        }


                }
            }
            int take = 4;
            int skip = (pageId - 1) * take;

            return Tuple.Create(await movies.Select(p => new GetMoviesInAdminDto
            {
                AgeRestriction = p.AgeRestriction,
                ImageSrc = p.ImageSrc,
                IsFree = p.IsFree,
                Time = p.Time,
                Title = p.Title,
                ViewCount = p.ViewCount,
                CreateDate = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                IsActive = p.IsActive,
                IsSlider = p.IsSlider,
                YearOfCreateDate = p.YearOfCreateDate,
                Director = p.Director,
                AvvrageRate = p.MovieComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

            }).Skip(skip).Take(take).ToListAsync(), movies.Count() / take, movies.Count());
        }

        public async Task<Tuple<List<GetMoviesInSiteDto>, int>> GetMoviesInSite(int pageId = 1, string filter = "", long categoryId = 0, long year = 0, string sort = "date")
        {
            var movies = _context.Movies.Include(p => p.MovieComments).Include(p => p.MovieCategories).ThenInclude(p => p.Category).Where(p => p.IsActive).OrderByDescending(p => p.CraeteDate).AsQueryable();
            if (await ExistsMovieCategory(categoryId))
            {
                var movieCategoryMovies = await _context.MovieCategoryMovies.Where(p => p.CategoryId == categoryId).Select(p => p.MovieId).ToListAsync();
                movies = movies.Where(p => movieCategoryMovies.Contains(p.Id)).AsQueryable();
            }
            if (!string.IsNullOrEmpty(filter))
                movies = movies.Where(p => p.Title.Contains(filter) || p.YearOfCreateDate.ToString().Contains(filter) || p.Director.Contains(filter)).AsQueryable();
            if (year != 0) movies = movies.Where(p => p.YearOfCreateDate == year).AsQueryable();
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
            return Tuple.Create(await movies.Select(p => new GetMoviesInSiteDto
            {
                DateTime = p.CraeteDate,
                Director = p.Director,
                FirstCategory = p.MovieCategories.FirstOrDefault().Category.Title,
                Title = p.Title,
                Id = p.Id,
                ImageName = p.ImageSrc,
                IsFree = p.IsFree,
                Year = p.YearOfCreateDate,
                AvvrageRate = p.MovieComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                    ((p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

            }).Skip(skip).Take(take).ToListAsync(), movies.Count() / take);
        }

        public async Task<string> GetMovieTitleById(long id)
        {
            return await _context.Movies.Where(p => p.Id == id).Select(p => p.Title).FirstOrDefaultAsync();
        }

        public async Task IncreaseMovieViewCount(long id)
        {
            var movie = await GetMovieById(id);
            if (movie.ViewCount == null) movie.ViewCount = 1;
            else movie.ViewCount++;
            await UpdateMovie(movie);
        }

        public async Task<bool> MovieExists(long id)
        {
            return await _context.Movies.AnyAsync(p => p.Id == id);
        }


        public async Task<Tuple<List<ShowMovieCommentDto>, int>> ShowMovieComment(long movieId, int pageId = 1)
        {
            int take = 5;
            int skip = (pageId - 1) * take;
            return Tuple.Create(await _context.MovieComments.Where(p => p.MovieId == movieId && p.ParentId == null).OrderByDescending(p => p.CraeteDate).Select(p => new ShowMovieCommentDto
            {
                CreateDate = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                Rate = p.Rate,
                Subject = p.Title,
                Text = p.Text,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                UserImage = _userService.GetUserImageByIdNoAsync(p.UserId),


            }).Skip(skip).Take(take).ToListAsync(), _context.MovieComments.Where(p => p.MovieId == movieId).Count() / take);
        }

        public async Task<Tuple<List<ShowMovieCommentDto>, int>> ShowMovieCommentsInAdmin(long movieId, int pageId = 1, string filter = "")
        {
            int take = 5;
            int skip = (pageId - 1) * take;
            var comments = _context.MovieComments.Where(p => p.MovieId == movieId && p.ParentId == null).OrderByDescending(p => p.CraeteDate).AsQueryable();
            if (!string.IsNullOrEmpty(filter)) comments = comments.Where(p => p.Title.Contains(filter) || p.Text.Contains(filter)).AsQueryable();
            return Tuple.Create(await comments.Select(p => new ShowMovieCommentDto
            {
                CreateDate = p.CraeteDate.ToShamsi(),
                Id = p.Id,
                Rate = p.Rate,
                Subject = p.Title,
                Text = p.Text,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                UserImage = _userService.GetUserImageByIdNoAsync(p.UserId),

            }).Skip(skip).Take(take).ToListAsync(), _context.MovieComments.Where(p => p.MovieId == movieId).Count() / take);
        }

        public async Task<ShowMovieInSiteDto> ShowMovieInSite(long id)
        {
            var movieCategories = await _context.MovieCategoryMovies.Include(p => p.Category).Where(p => p.MovieId == id).Select(p => p.Category.Title).ToListAsync();

            return await _context.Movies.Include(p => p.MovieComments).Include(p => p.MovieFiles).Where(p => p.Id == id).Select(p => new ShowMovieInSiteDto
            {
                Id = p.Id,
                AgeRestriction = p.AgeRestriction,
                Description = p.Description,
                ImageName = p.ImageSrc,
                Time = p.Time,
                Tiser = p.TiserFile,
                Title = p.Title,
                Year = p.YearOfCreateDate,
                Categories = movieCategories,
                IsActive = p.IsActive,
                AvvrageRate = p.MovieComments.Where(x => x.Rate != null && x.Rate > 0 && x.Rate <= 10).Sum(z => z.Rate) /
                        ((p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() > 0) ? p.MovieComments.Where(o => o.Rate != null && o.Rate > 0 && o.Rate <= 10).Count() : 1)

                    ,
                MovieFiles = p.MovieFiles.Select(x => new ShowMovieFileInSiteDto { FileName = x.FileName, Quality = x.Quality }).ToList(),
                IsFree = p.IsFree
            }).FirstOrDefaultAsync();
        }

        public async Task<List<ShowMovieCommentDto>> ShowSubMovieComments(long movieId)
        {
            return await _context.MovieComments.Where(p => p.MovieId == movieId && p.ParentId != null).OrderByDescending(p => p.CraeteDate).Select(p => new ShowMovieCommentDto
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

        public async Task<List<ShowMovieCommentDto>> ShowSubMovieCommentsInAdmin(long? parentId)
        {
            return await _context.MovieComments.Where(p => p.ParentId == parentId).Select(p => new ShowMovieCommentDto
            {
                Id = p.Id,
                CreateDate = p.CraeteDate.ToShamsi(),
                Subject = p.Title,
                Text = p.Text,
                UserName = _userService.GetUserNameByIdNoAsync(p.UserId),
                UserImage = _userService.GetUserImageByIdNoAsync(p.UserId),

            }).ToListAsync();
        }

        public async Task UpdateMovie(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieCategory(MovieCategory movieCategory)
        {
            _context.MovieCategories.Update(movieCategory);
            await _context.SaveChangesAsync();
        }
    }
}
