using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace RedFolder.Blog.Markdown.Transformers
{
    public class CodeTransformer: BaseTransformer
    {
        public CodeTransformer() : base()
        {

        }

        public CodeTransformer(ITransformer innerTransformer) : base(innerTransformer)
        {

        }

        protected override string PostTransform(JObject meta, string markdown)
        {
            //return Regex.Replace(markdown, "<code>(.*)</code>", m => m.Value.Replace("\n", "<br/>\n"), RegexOptions.Singleline);
            return markdown.Replace("<code>", "<pre><code>").Replace("</code>", "</code></pre>").Replace("<code>\n","<code>");
        }
    }
}
