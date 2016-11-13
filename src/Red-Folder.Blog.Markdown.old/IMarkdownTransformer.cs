using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.Blog.Markdown
{
    public interface IMarkdownTransformer
    {
        RedFolder.Website.Data.Blog Transform(JObject meta, string markdown);
    }
}
