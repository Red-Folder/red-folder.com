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
        public IActionResult Weekly([FromServices] IActivityRepository repository, int year, int weekNumber)
        {
            var raw = repository.Weekly(year, weekNumber);

            var grid = new string[][]
            {
                new string[] {"A1", "B1", "C1" },
                new string[] {"A2", "B2", "B2" }
            };

            var viewModel = new WeekSummary
            {
                Year = raw.Year,
                WeekNumber = raw.WeekNumber,
                Start = raw.Start,
                End = raw.End,
                PodCasts = new ActivityLayout<Models.Activity.PodCastActivity>(raw.PodCasts, "A2"),
                Skills = new ActivityLayout<Models.Activity.SkillsActivity>(raw.Skills, "B1"),
                Pluralsight = new ActivityLayout<Models.Activity.PluralsightActivity>(raw.Pluralsight, "B2"),
                Focus = new ActivityLayout<Models.Activity.FocusActivity>(raw.Focus, "A1"),
                Clients = new ActivityLayout<Models.Activity.ClientActivity>(raw.Clients, "C1"),
                Layout = grid
            };

            return View("Weekly", viewModel);
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
    }
}
