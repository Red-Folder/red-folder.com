using System.Collections.Generic;

namespace RedFolder.Blog
{
    public interface ISiteMapUrlRepository
    {
        List<string> GetUrls();
    }
}
