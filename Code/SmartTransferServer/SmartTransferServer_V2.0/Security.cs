using System;
using System.Collections.Generic;

namespace SmartTransferServer_V2._0
{
    internal class Security
    {
        public Security()
        {
        }

        internal bool PathIsAllowed(List<string> allRootPaths, string requestPath)
        {
            foreach(string path in allRootPaths)
            {
                if (requestPath.IndexOf(path) == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}