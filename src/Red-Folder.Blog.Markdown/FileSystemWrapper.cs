using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.Blog.Markdown
{
    public class FileSystemWrapper : IFileSystem
    {
        public bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        public bool FolderExists(string folder)
        {
            return Directory.Exists(folder);
        }

        public string GetFileContents(string filename)
        {
            return File.ReadAllText(filename);
        }

        public List<string> GetFiles(string folder)
        {
            return Directory.GetFiles(folder).ToList();
        }

        public List<string> GetFolders(string baseFolder)
        {
            return Directory.GetDirectories(baseFolder).ToList();
        }
    }
}
