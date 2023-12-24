using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace RedFolder.Blog.Markdown
{
    public class BlogLoader : IBlogLoader
    {
        private IFileSystem _fileSystem;
        private IMarkdownTransformer _markdownTransformer;

        public BlogLoader(IFileSystem fileSystem, IMarkdownTransformer markdownTransformer)
        {
            _fileSystem = fileSystem;
            _markdownTransformer = markdownTransformer;
        }

        public Website.Data.Blog GetBlog(string folder)
        {
            if (_fileSystem.FolderExists(folder))
            {
                var files = _fileSystem.GetFiles(folder);

                if (files != null && files.Count() > 0)
                {
                    if (files.Where(f => f.EndsWith(".md")).Count() == 1 && files.Where(f => f.EndsWith(".json")).Count() == 1)
                    {
                        var markdown = _fileSystem.GetFileContents(files.Where(f => f.EndsWith(".md")).First());
                        var metaContent = _fileSystem.GetFileContents(files.Where(f => f.EndsWith(".json")).First());

                        var meta = JObject.Parse(metaContent);

                        return _markdownTransformer.Transform(meta, markdown);
                    }
                }

                return null;            
            }

            throw new InvalidOperationException("Invalid folder");
        }


    }
}
