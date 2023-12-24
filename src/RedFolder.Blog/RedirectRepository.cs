using RedFolder.Website.Data;
using System.Collections.Generic;

namespace RedFolder.Blog
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
