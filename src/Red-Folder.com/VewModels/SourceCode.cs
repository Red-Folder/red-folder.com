using System;

namespace RedFolder.ViewModels
{
    public class SourceCode : BlogArticle
    {
        public SourceCode(string title, Uri link, Paragraphs description)
            : base(title, link, description)
        {
        }
    }
}