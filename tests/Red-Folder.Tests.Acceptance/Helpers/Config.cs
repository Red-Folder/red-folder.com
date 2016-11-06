using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Red_Folder.Tests.Acceptance.Helpers
{
    public class Config
    {
        private static string _host = null;

        public static string Host
        {
            get {
                if (_host == null)
                {
                    _host = Environment.GetEnvironmentVariable("REDFOLDER_HOST");
                    if (_host == null)
                    {
                        _host = "https://www.red-folder.com";
                    }
                }

                return _host;
            }
        }
    }
}
