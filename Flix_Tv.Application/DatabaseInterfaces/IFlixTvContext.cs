using Flix_Tv.Domain.Entites.Movies;
using Flix_Tv.Domain.Entites.Permissions;
using Flix_Tv.Domain.Entites.Plans;
using Flix_Tv.Domain.Entites.Roles;
using Flix_Tv.Domain.Entites.Serials;
using Flix_Tv.Domain.Entites.SettingSite;
using Flix_Tv.Domain.Entites.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flix_Tv.Application.DatabaseInterfaces
{
  public   interface IFlixTvContext
    {
        #region Users
          DbSet<User> Users { get; set; }
          DbSet<UserFavoriteMovie> UserFavoriteMovies { get; set; }
          DbSet<UserFavoriteSerial> UserFavoriteSerials { get; set; }
          DbSet<UserLoginLog> UserLoginLogs { get; set; }
          DbSet<UserRole> UserRoles { get; set; }
          DbSet<Wallet> Wallets { get; set; }
        #endregion

        #region SettingsSite
          DbSet<AboutUs> AboutUs { get; set; }
          DbSet<ContactUs> ContactUs { get; set; }
          DbSet<AboutUsFeature> AboutUsFeatures { get; set; }
        #endregion


        #region Serials
          DbSet<Serial> Serials { get; set; }
          DbSet<SerialCategory> SerialCategories { get; set; }
          DbSet<SerialCategorySerial> SerialCategorySerials { get; set; }
          DbSet<SerialEpisode> SerialEpisodes { get; set; }
          DbSet<SerialEpisodeFile> SerialEpisodeFiles { get; set; }
          DbSet<SerialSeason> SerialSeasons { get; set; }
          DbSet<SerialComment> SerialComments { get; set; }
          DbSet<SerialEpisodeComment> SerialEpisodeComments { get; set; }
        #endregion

        #region Roles
          DbSet<Role> Roles { get; set; }
          DbSet<RolePermission> RolePermissions { get; set; }

        #endregion


        #region Plans
          DbSet<Plan> Plans { get; set; }
        #endregion


        #region Permissions
          DbSet<Permission> Permissions { get; set; }
        #endregion


        #region Movies
          DbSet<Movie> Movies { get; set; }
          DbSet<MovieCategory> MovieCategories { get; set; }
          DbSet<MovieCategoryMovie> MovieCategoryMovies { get; set; }
          DbSet<MovieComment> MovieComments { get; set; }
          DbSet<MovieFile> MovieFiles { get; set; }
        #endregion

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
