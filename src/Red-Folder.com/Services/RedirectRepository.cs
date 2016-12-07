using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedFolder.Website.Data;

namespace RedFolder.Services
{
    public class RedirectRepository : IRedirectRepository
    {
        private List<IRedirectRepository> _subRepositories;

        public RedirectRepository(List<IRedirectRepository> subRepositories)
        {
            _subRepositories = subRepositories;
        }

        public Dictionary<string, List<Redirect>> GetRedirects()
        {
            if (_subRepositories == null)
            {
                return null;
            }
            else
            {
                var redirects = new Dictionary<string, List<Redirect>>();
                foreach (var subRepository in _subRepositories)
                {
                    foreach (var redirect in subRepository.GetRedirects())
                    {
                        redirects.Add(redirect.Key, redirect.Value);
                    }
                }
                return redirects;
            }
        }
    }
}
