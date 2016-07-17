using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.Data
{
    public class RepoContextSeedData
    {
        private RepoContext _context;

        public RepoContextSeedData(RepoContext context)
        {
            _context = context;
        }

        public void EnsureSeedData()
        {
            if(!_context.Repos.Any())
            {
                var repo1 = new Repo
                {
                    Name = "Repo 1",
                    Description = "Repo 1 desciption",
                    Stars = 5,
                    OpenIssues = 7,
                    Tags = new List<Tag>
                                {
                                    new Tag { Description = "Cordova" },
                                    new Tag { Description = "Java" }
                                }
                    };

                var repo2 = new Repo
                {
                    Name = "Repo 2",
                    Description = "Repo 2 desciption",
                    Stars = 3,
                    OpenIssues = 0,
                    Tags = new List<Tag>
                                {
                                    new Tag { Description = "Cordova" },
                                    new Tag { Description = "JavaScript" },
                                    new Tag { Description = "GPS" }
                                }
                };

                var repo3 = new Repo
                {
                    Name = "Repo 3",
                    Description = "Repo 3 desciption",
                    Stars = 10,
                    OpenIssues = 100,
                    Tags = new List<Tag>
                                {
                                    new Tag { Description = "C#" },
                                    new Tag { Description = "JavaScript" },
                                    new Tag { Description = "CSS" }
                                }
                };

                _context.Repos.Add(repo1);
                _context.Repos.Add(repo2);
                _context.Repos.Add(repo3);
                _context.Tags.AddRange(repo1.Tags);
                _context.Tags.AddRange(repo2.Tags);
                _context.Tags.AddRange(repo3.Tags);

                _context.SaveChanges();
            }
        }
    }
}
