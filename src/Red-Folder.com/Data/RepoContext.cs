using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RedFolder.Data
{
    public class RepoContext: DbContext
    {
        private IConfigurationRoot _config;

        public RepoContext(IConfigurationRoot config, DbContextOptions options): base(options)
        {
            _config = config;
            Database.Migrate();
        }

        public DbSet<Repo> Repos { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //var connString = _config["Data:RepoContext:ConnectionString"];
            var connString = _config.GetConnectionString("RepoContext");

            optionsBuilder.UseSqlServer(connString);

        }
    }
}
