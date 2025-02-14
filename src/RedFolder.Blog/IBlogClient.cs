using System.Collections.Generic;

namespace RedFolder.Blog
{
    public interface IBlogClient
    {
        IList<Website.Data.Blog> GetAll();
        string LoadContent(Website.Data.Blog blog);
    }
}