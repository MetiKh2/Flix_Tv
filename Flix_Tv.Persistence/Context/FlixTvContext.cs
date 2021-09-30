using Flix_Tv.Application.DatabaseInterfaces;
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
using System.Threading.Tasks;

namespace Flix_Tv.Persistence.Context
{
 public   class FlixTvContext:DbContext,IFlixTvContext
    {
        public FlixTvContext(DbContextOptions<FlixTvContext> options) : base(options)
        { }

        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<UserFavoriteMovie> UserFavoriteMovies { get; set; }
        public DbSet<UserFavoriteSerial> UserFavoriteSerials { get; set; }
        public DbSet<UserLoginLog> UserLoginLogs { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        #endregion

        #region SettingsSite
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<AboutUsFeature> AboutUsFeatures { get; set; }
        #endregion


        #region Serials
        public DbSet<Serial> Serials { get; set; }
        public DbSet<SerialCategory> SerialCategories { get; set; }
        public DbSet<SerialCategorySerial> SerialCategorySerials { get; set; }
        public DbSet<SerialEpisode> SerialEpisodes { get; set; }
        public DbSet<SerialEpisodeFile> SerialEpisodeFiles { get; set; }
        public DbSet<SerialSeason> SerialSeasons { get; set; }
        public DbSet<SerialComment> SerialComments { get; set; }
        public DbSet<SerialEpisodeComment> SerialEpisodeComments { get; set; }
        #endregion

        #region Roles
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion


        #region Plans
        public DbSet<Plan> Plans { get; set; }
        #endregion


        #region Permissions
        public DbSet<Permission> Permissions { get; set; }
        #endregion


        #region Movies
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }
        public DbSet<MovieCategoryMovie> MovieCategoryMovies { get; set; }
        public DbSet<MovieComment> MovieComments { get; set; }
        public DbSet<MovieFile> MovieFiles { get; set; }
        #endregion

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
             .SelectMany(t => t.GetForeignKeys())
             .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<User>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<RolePermission>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<UserRole>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<MovieCategory>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<SerialCategory>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<Movie>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<MovieCategoryMovie>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<MovieFile>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<SerialCategorySerial>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<Serial>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<SerialSeason>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<SerialEpisode>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<SerialEpisodeFile>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<UserFavoriteMovie>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<UserFavoriteSerial>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<Plan>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<ContactUs>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<MovieComment>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<SerialComment>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<SerialEpisodeComment>().HasQueryFilter(p=>!p.IsRemoved);
        }
    }
}
