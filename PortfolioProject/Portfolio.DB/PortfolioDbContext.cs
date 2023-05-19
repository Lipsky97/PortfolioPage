using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Portfolio.DB.Config;
using Portfolio.DB.Models;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace Portfolio.DB
{
    public class PortfolioDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<CV> CVs { get; set; }
        public virtual DbSet<PortfolioProject> Projects { get; set; }
        public virtual DbSet<PortfolioPicture> PortfolioPictures { get; set; }

        public PortfolioDbContext() 
        {

        }

        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) { return; }

            var settings = ConfigurationManager.GetSection("dbConfigurationStrings") as NameValueCollection;
            var dic = settings.AllKeys.ToDictionary(k => k, k => settings[k]);

            optionsBuilder.UseNpgsql(dic["connectionString"]);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortfolioProject>()
                .HasMany(p => p.Pictures)
                .WithOne()
                .HasForeignKey(p => p.ProjectId);
        }


    }
}
