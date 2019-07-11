using Red_Folder.com.Models.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.Services
{
    public class ActivityRepository : IActivityRepository
    {
        private ActivityClient _client;

        public ActivityRepository(string activityUrl, string activityCode)
        {
            _client = new ActivityClient(activityUrl, activityCode);
        }

        public Weekly Weekly(string year, string weekNumber)
        {
            var week = _client.Weekly(year, weekNumber);

            week.Books = new BookActivity();
            week.Books.Books = new List<Book>
            {
                new Book
                {
                    Title = "War and Peace and IT: Business Leadership, Technology, and Success in the Digital Age",
                    Author = "Mark Schwartz",
                    Description = "Advice for business leaders looking to lead their company into the digital age by harnessing the expertise and innovation that is already under their roof.",
                    ImageUrl = "https://images-eu.ssl-images-amazon.com/images/I/41i6bnKdwUL.jpg"
                },
                new Book
                {
                    Title = "The Riftwar Saga Trilogy",
                    Author = "Raymond E. Feist",
                    Description = "Fantasy novel follow Pug and Tomas as suddenly the peace of the Kingdom is destroyed as mysterious alien invaders swarm the land",
                    ImageUrl = "https://images-eu.ssl-images-amazon.com/images/I/41fNqtayNWL.jpg"
                },
            };

            return week;
        }
    }
}
