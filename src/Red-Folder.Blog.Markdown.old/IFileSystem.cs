using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.Blog.Markdown
{
    public interface IFileSystem
    {
        bool FolderExists(string folder);
        bool FileExists(string filename);

        List<string> GetFolders(string baseFolder);
        List<string> GetFiles(string folder);
        string GetFileContents(string filename);
    }
}
