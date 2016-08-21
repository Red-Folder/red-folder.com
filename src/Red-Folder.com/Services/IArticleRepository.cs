using RedFolder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.Services
{
    public interface IArticleRepository
    {
        IEnumerable<BlogArticle> GetROIArticles();
        IEnumerable<BlogArticle> GetAspNetCoreArticles();
    }
}
