using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.Blog.Markdown.Tests.Unit.Helpers
{
    public class FileSystemBuilder
    {
        private Mock<IFileSystem> _mock;

        private Dictionary<string, List<string>> _folderStructures;
        private Dictionary<string, List<string>> _fileStructures;

        public FileSystemBuilder()
        {
            _mock = new Mock<IFileSystem>();
            _folderStructures = new Dictionary<string, List<string>>();
            _fileStructures = new Dictionary<string, List<string>>();
        }

        public FileSystemBuilder AddFolderStructure(string baseFolder, List<string> folders)
        {
            _folderStructures.Add(baseFolder, folders);
            return this;
        }

        public FileSystemBuilder AddInvalidFolder(string baseFolder)
        {
            _mock.Setup(m => m.FolderExists(baseFolder)).Returns(false);

            return this;
        }

        public FileSystemBuilder AddFileContents(string folder, string filename, string contents)
        {
            var fullfilename = folder + @"\" + filename;

            if (_fileStructures.ContainsKey(folder))
            {
                _fileStructures[folder].Add(fullfilename);
            }
            else
            {
                _fileStructures.Add(folder, new List<string> { fullfilename });
            }

            _mock.Setup(m => m.FileExists(fullfilename)).Returns(true);
            _mock.Setup(m => m.GetFileContents(fullfilename)).Returns(contents);

            return this;
        }

        public IFileSystem Build()
        {
            if (_folderStructures.Count > 0)
            {
                foreach (var folder in _folderStructures)
                {
                    _mock.Setup(m => m.FolderExists(folder.Key)).Returns(true);
                    _mock.Setup(m => m.GetFolders(folder.Key)).Returns(folder.Value);
                }
            }

            if (_fileStructures.Count > 0)
            {
                foreach (var folder in _fileStructures)
                {
                    _mock.Setup(m => m.FolderExists(folder.Key)).Returns(true);
                    _mock.Setup(m => m.GetFiles(folder.Key)).Returns(folder.Value);
                }
            }

            return _mock.Object;
        }
    }
}
