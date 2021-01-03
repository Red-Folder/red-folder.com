using Microsoft.AspNetCore.Mvc;
using Red_Folder.com.Services;
using Red_Folder.com.ViewModels.Activity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.com.Controllers.Web
{
    public class ActivityController : Controller
    {
        [HttpGet("Activity/Weekly/{year}/{weekNumber}")]
        public IActionResult Weekly([FromServices] IActivityRepository repository, string year, string weekNumber)
        {
            var raw = repository.Weekly(year, weekNumber);

            var viewModel = new WeekSummary
            {
                Year = raw.Year,
                WeekNumber = raw.WeekNumber,
                Start = raw.Start,
                End = raw.End,
                PodCasts = new ActivityLayout<Models.Activity.PodCastActivity>(raw.PodCasts, "podcasts", x => x != null && x.Categories != null && x.Categories.Count > 0),
                Skills = new ActivityLayout<Models.Activity.SkillsActivity>(raw.Skills, "skills", x => x != null && x.Skills != null && x.Skills.Count > 0),
                Pluralsight = new ActivityLayout<Models.Activity.PluralsightActivity>(raw.Pluralsight, "pluralsight", x => x != null && x.Courses != null && x.Courses.Count > 0),
                Focus = new ActivityLayout<Models.Activity.FocusActivity>(raw.Focus, "focus", x => x != null && x.Focus != null && x.Focus.Count > 0),
                //Clients = new ActivityLayout<Models.Activity.ClientActivity>(raw.Clients, "clients", x => x != null && x.Clients != null && x.Clients.Count > 0),
                Blogs = new ActivityLayout<Models.Activity.BlogActivity>(raw.Blogs, "blogs", x => x != null && x.Blogs != null && x.Blogs.Count > 0),
                Events = new ActivityLayout<Models.Activity.EventActivity>(raw.Events, "events", x => x != null && x.Events != null && x.Events.Count > 0),
                Books = new ActivityLayout<Models.Activity.BookActivity>(raw.Books, "books", x => x != null && x.Books != null && x.Books.Count > 0)
            };

            viewModel.Layout = GetGrid(viewModel);

            return View("Weekly", viewModel);
        }

        private string[][] GetGrid(WeekSummary model)
        {
            if (model.Events.Display)
            {
                if (!model.Pluralsight.Display && !model.Blogs.Display)
                {
                    return new string[][]
                    {
                        new string[] { "focus", "skills" },
                        new string[] { "title", "title" },
                        new string[] { "events", "events" },
                        new string[] { "footer", "footer" },
                        new string[] { "podcasts", "books" }
                    };
                }

                return new string[][]
                {
                    new string[] {"focus", "skills", "books" },
                    new string[] {"title", "title", "title"},
                    new string[] { "events", "events", "events" },
                    new string[] { "footer", "footer", "footer" },
                    new string[] {"podcasts", "pluralsight", "pluralsight" },
                    new string[] {"podcasts", "blogs", "blogs" },
                };
            }

            if (!model.Pluralsight.Display && !model.Blogs.Display)
            {
                return new string[][]
                {
                    new string[] { "focus", "skills" },
                    new string[] { "title", "title" },
                    new string[] { "podcasts", "books" },
                    new string[] { "footer", "footer" }
                };
            }

            return new string[][]
            {
                new string[] {"focus", "skills", "books" },
                new string[] {"title", "title", "title"},
                new string[] {"podcasts", "pluralsight", "pluralsight" },
                new string[] {"podcasts", "blogs", "blogs" },
                new string[] {"footer", "footer", "footer"}
            };
        }

        private static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }

        //[HttpGet("Activity/Books/{year}")]
        //public async Task<IActionResult> BooksInYear([FromServices] IActivityRepository repository, string year)
        //{
        //    var books = await repository.BooksInYear(year);
        //    return View(books);
        //}

        //[HttpGet("Activity/Skills/{year}")]
        //public async Task<IActionResult> SkillsInYear([FromServices] IActivityRepository repository, string year)
        //{
        //    var skills = await repository.SkillsInYear(year);
        //    return View(skills);
        //}
    }
}
