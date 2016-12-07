using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RedFolder.Blog.Markdown.Transformers
{
    public class GistTransformer : BaseTransformer
    {
        public GistTransformer() : base()
        {

        }

        public GistTransformer(ITransformer innerTransformer) : base(innerTransformer)
        {

        }

        protected override string PostTransform(JObject meta, string markdown)
        {
            return Regex.Replace(markdown, "%\\[(.*?)\\]", m => "<script src=\"" + m.Groups[1].Value + "\"></script>");
        }
    }
}
