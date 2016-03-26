using System.Collections.Generic;

namespace RedFolder.ViewModels
{
    public class CordovaProject
    {
        public string Title { get; private set; }
        public Paragraphs Description { get; private set; }

        public List<BlogArticle> Articles { get; private set; }
        public List<SourceCode> SourceCode { get; private set; }

        public CordovaProject(string title, Paragraphs description, List<BlogArticle> articles, List<SourceCode> sourceCode)
        {
            Title = title;
            Description = description;
            Articles = articles;
            SourceCode = sourceCode;
        }
    }
}