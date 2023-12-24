using System.Collections.Generic;

namespace RedFolder.Blog
{
    public interface IBlogRepository
    {
        IList<Website.Data.Blog> GetAll();
        Website.Data.Blog Get(string url);
        string LoadContent(Website.Data.Blog blog);

        void Reload();
    }
}
