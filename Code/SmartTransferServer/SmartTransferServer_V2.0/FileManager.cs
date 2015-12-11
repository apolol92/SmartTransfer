using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTransferServer_V2._0
{
    class FileManager
    {
        public string loadFile(string category, string filename)
        {
            return File.ReadAllBytes(filename).ToString();
        }

        public void saveFile(string filename, string data)
        {
            byte[] bData = Encoding.ASCII.GetBytes(data);
            File.WriteAllBytes(filename, bData);
        }

        public void deleteFile(string filename)
        {
            System.IO.File.Delete(filename);
        }

        public List<string> listAllFilesInCategoryFolders(List<string> allPaths)
        {
            List<String> allFiles = new List<string>();
            for (int i = 0; i < allPaths.Count; i++)
            {
                try
                {
                    foreach (var path in Directory.GetFiles(allPaths[i]))
                    {
                        allFiles.Add(path);
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return allFiles;
        }
    }
}
