using Red_Folder.com.Models.Activity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.Services
{
    public class ActivityRepository : IActivityRepository
    {
        private ActivityClient _client;

        public ActivityRepository(ActivityClient client)
        {
            _client = client;
        }

        public Weekly Weekly(string year, string weekNumber)
        {
            return _client.Weekly(year, weekNumber).Result;
        }

        public async Task<List<Book>> BooksInYear(string year)
        {
            var books = new List<Book>();
            for (int i = 1; i <= 53; i++)
            {
                var weekActivity = await _client.Weekly(year, i.ToString("D2"));

                if (weekActivity?.Books?.Books != null)
                {
                    foreach (var book in weekActivity.Books.Books)
                    {
                        if (!books.Any(x => x.ImageUrl == book.ImageUrl))
                        {
                            books.Add(book);
                        }
                    }
                }
            }

            return books;
        }

        public async Task<List<Skill>> SkillsInYear(string year)
        {
            var skills = new List<Skill>();
            for (int i = 1; i <= 53; i++)
            {
                var weekActivity = await _client.Weekly(year, i.ToString("D2"));

                if (weekActivity?.Skills?.Skills != null)
                {
                    foreach (var skill in weekActivity.Skills.Skills)
                    {
                        var existing = skills.Find(x => x.Name == skill.Name);

                        if (existing == null)
                        {
                            skills.Add(skill);
                        }
                        else
                        {
                            existing.TotalDuration += skill.TotalDuration;
                        }
                    }
                }
            }

            return skills;
        }
    }
}
