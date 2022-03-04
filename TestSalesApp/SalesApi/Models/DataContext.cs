using Microsoft.EntityFrameworkCore;

namespace SalesApi.Models
{
    public class DataContext:DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("PostgresString"));
        }

        public DbSet<Muffin> Muffins { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Muffin>().ToTable("muffin");
        }

    }
}
