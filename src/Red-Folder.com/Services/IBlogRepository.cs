using RedFolder.Website.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.Services
{
    public interface IBlogRepository
    {
        IList<RedFolder.Website.Data.Blog> GetAll();
        RedFolder.Website.Data.Blog Get(string url);
        string LoadContent(Website.Data.Blog blog);

        void Reload();
    }
}
