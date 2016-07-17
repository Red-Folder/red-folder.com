using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Extensions.OptionsModel;
using RedFolder.Models;

namespace RedFolder.Data
{
    public class RepoContext: DbContext
    {
        public RepoContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Repo> Repos { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Startup.Configuration["Data:RepoContext:ConnectionString"];

            optionsBuilder.UseSqlServer(connString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
