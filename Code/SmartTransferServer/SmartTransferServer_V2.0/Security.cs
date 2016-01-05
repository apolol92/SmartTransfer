using System;
using System.Collections.Generic;

namespace SmartTransferServer_V2._0
{
    internal class Security
    {
        public Security()
        {
        }

        public bool PathIsAllowed(List<string> allRootPaths, string requestPath)
        {
            foreach(string path in allRootPaths)
            {
                if (path.IndexOf(requestPath) != -1)
                {

                    return true;
                }
            }
            return false;
        }

        
    }
}