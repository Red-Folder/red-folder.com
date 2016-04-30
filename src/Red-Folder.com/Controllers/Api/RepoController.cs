using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RedFolder.Services;
using RedFolder.Models.Api;
using System.Threading;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RedFolder.Controllers.Api
{
    [Route("api/[controller]")]
    public class RepoController : Controller
    {
        [FromServices]
        public IRepoRepository Repos { get; set; }

        [HttpGet]
        public IEnumerable<Repo> Get()
        {
            //Thread.Sleep(10 * 1000);
            return Repos.GetAll();
        }

    }
}
