using System;

namespace RedFolder.ViewModels
{
    public class BlogArticle
    {
        public string Title { get; private set; }
        public Uri Link { get; private set; }
        public Paragraphs Description { get; private set; }

        public BlogArticle(string title, Uri link, Paragraphs description)
        {
            Title = title;
            Link = link;
            Description = description;
        }
    }
}