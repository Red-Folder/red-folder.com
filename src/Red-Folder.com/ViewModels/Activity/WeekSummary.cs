using Red_Folder.com.Models.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Folder.com.ViewModels.Activity
{
    public class WeekSummary
    {
        public int Year { get; set; }

        public int WeekNumber { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public ActivityLayout<PodCastActivity> PodCasts { get; set; }

        public ActivityLayout<SkillsActivity> Skills { get; set; }

        public ActivityLayout<PluralsightActivity> Pluralsight { get; set; }

        public ActivityLayout<FocusActivity> Focus { get; set; }

        public ActivityLayout<ClientActivity> Clients { get; set; }

        public ActivityLayout<BlogActivity> Blogs { get; set; }

        public string[][] Layout { get; set; }

        public string GridTemplateAreas
        {
            get
            {
                var css = new StringBuilder();

                foreach (var row in Layout)
                {
                    css.Append('"');
                    foreach (var column in row)
                    {
                        css.Append(column);
                        css.Append(' ');
                    }
                    css.Append('"');
                }

                return css.ToString();
            }
        }

        public string GridTemplateColumns
        {
            get
            {
                var css = new StringBuilder();

                css.Append('"');
                for (int i = 0; i < Layout[0].Length; i++)
                {
                    css.Append($"auto ");
                }
                css.Append('"');

                return css.ToString();
            }
        }

        public string GridAreas
        {
            get
            {
                var css = new StringBuilder();

                foreach (var row in Layout)
                {
                    foreach (var column in row)
                    {
                        css.AppendLine($"#{column} {{");
                        css.AppendLine($"grid-area: {column};");
                        css.AppendLine($"}}");
                    }
                }

                return css.ToString();
            }
        }
    }
}
