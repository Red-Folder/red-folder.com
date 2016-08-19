using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RedFolder.Services;
using RedFolder.Models.Api;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RedFolder.Controllers.Api
{
    [Route("api/[controller]")]
    public class RepoController : Controller
    {
        [HttpGet]
        public IEnumerable<Repo> Get([FromServices] IRepoRepository repos)
        {
            //Thread.Sleep(10 * 1000);
            return repos.GetAll();
        }

    }
}
