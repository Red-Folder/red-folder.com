using System;
using System.Collections.Generic;

namespace RedFolder.Blog.Markdown
{
    public class MarkdownRepository
    {
        private IFileSystem _fileSystem;
        private IBlogLoader _blogLoader;

        public MarkdownRepository()
        {
            throw new NotImplementedException("Not yet added the REAL filesystem wrapper");
        }

        public MarkdownRepository(IFileSystem fileSystem, IBlogLoader blogLoader)
        {
            _fileSystem = fileSystem;
            _blogLoader = blogLoader;
        }

        public IList<RedFolder.Website.Data.Blog> Import(string baseFolder)
        {
            if (_fileSystem.FolderExists(baseFolder))
            {
                return LoadBlogs(baseFolder);
            }

            throw new InvalidOperationException("Invalid folder");
        } 

        private List<RedFolder.Website.Data.Blog> LoadBlogs(string baseFolder)
        {
            var blogs = new List<RedFolder.Website.Data.Blog>();

            foreach (var folder in _fileSystem.GetFolders(baseFolder))
            {
                try
                {
                    var blog = _blogLoader.GetBlog(folder);
                    if (blog != null)
                    {
                        blogs.Add(blog);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return blogs;
        }
    }
}
