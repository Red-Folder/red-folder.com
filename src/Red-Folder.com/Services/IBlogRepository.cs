using RedFolder.Website.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.Services
{
    public interface IBlogRepository
    {
        IList<Blog> GetAll();
        Blog Get(string url);
    }
}
