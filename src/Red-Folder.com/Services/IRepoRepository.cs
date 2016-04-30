using RedFolder.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.Services
{
    public interface IRepoRepository
    {
        IEnumerable<Repo> GetAll();
    }
}
