using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.Services
{
    public class RepoRepository : IRepoRepository
    {
        private RedFolder.Data.RepoContext _context = null;
        private ILogger<RepoRepository> _logger = null;

        public RepoRepository(RedFolder.Data.RepoContext context, ILogger<RepoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<RedFolder.Models.Api.Repo> GetAll()
        {
            try
            {
                var result = from repo in _context.Repos
                             select new RedFolder.Models.Api.Repo
                             {
                                 Name = repo.Name,
                                 Description = repo.Description,
                                 Stars = repo.Stars,
                                 OpenIssues = repo.OpenIssues,
                                 Tags = (from tag in repo.Tags
                                         select tag.Description).ToList()
                             };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get Repos from the Database", ex);
                return null;
            }
        }
    }
}
