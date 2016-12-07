using RedFolder.Website.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.Services
{
    public interface IRedirectRepository
    {
        Dictionary<string, List<Redirect>> GetRedirects();
    }
}
