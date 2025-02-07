namespace RedFolder.ViewModels
{
    public class Certification
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }

        public Certification(string title, string date, string description, string imageUrl, string link)
        {
            Title = title;
            Date = date;
            Description = description;
            ImageUrl = imageUrl;
            Link = link;
        }
    }
}
