using RedFolder.Website.Data;
using System.Collections.Generic;

namespace RedFolder.Blog
{
    public interface IRedirectRepository
    {
        Dictionary<string, List<Redirect>> GetRedirects();
    }
}
