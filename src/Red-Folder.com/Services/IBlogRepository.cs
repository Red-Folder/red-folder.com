using Red_Folder.Website.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.Services
{
    public interface IBlogRepository
    {
        Blog Get(string url);
    }
}
