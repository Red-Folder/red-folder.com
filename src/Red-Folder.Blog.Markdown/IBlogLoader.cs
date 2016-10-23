using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.Blog.Markdown
{
    public interface IBlogLoader
    {
        RedFolder.Website.Data.Blog GetBlog(string folder);
    }
}
