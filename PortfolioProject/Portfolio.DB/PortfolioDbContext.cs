using Microsoft.EntityFrameworkCore;
using Portfolio.DB.Models;

namespace Portfolio.DB
{
    public class PortfolioDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<CV> CVs { get; set; }

        public PortfolioDbContext() 
        {

        }

        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) { return; }

            optionsBuilder.UseNpgsql("host=portfoliodb.c3giaa8kwm1w.eu-central-1.rds.amazonaws.com;port=5432;user id=lipsky;password=mojabazadanych123;database=postgres;");
            base.OnConfiguring(optionsBuilder);
        }


    }
}
