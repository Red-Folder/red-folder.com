using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedFolder.Models.Api;

namespace RedFolder.Services
{
    public class RepoRepository : IRepoRepository
    {
        public IEnumerable<Repo> GetAll()
        {
            return new List<Repo>
            {
                new Repo
                {
                     Name = "Repo 1",
                     Description = "Repo 1 desciption",
                     Stars = 5,
                     OpenIssues = 7,
                     Tags = new List<string>
                     {
                        "Cordova",
                        "Java"
                     }
                },

                new Repo
                {
                     Name = "Repo 2",
                     Description = "Repo 2 desciption",
                     Stars = 3,
                     OpenIssues = 0,
                     Tags = new List<string>
                     {
                        "Cordova",
                        "JavaScript",
                        "GPS"
                     }
                },

                new Repo
                {
                     Name = "Repo 3",
                     Description = "Repo 3 desciption",
                     Stars = 10,
                     OpenIssues = 100,
                     Tags = new List<string>
                     {
                        "C#",
                        "CSS",
                        "JavaScript"
                     }
                }
            };
        }
    }
}
