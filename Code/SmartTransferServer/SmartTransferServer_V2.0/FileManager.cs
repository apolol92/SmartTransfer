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
            //return Encoding.UTF8.GetString(Convert.FromBase64String(System.Text.Encoding.Bas.GetString(File.ReadAllBytes(filename))));
            return System.Text.Encoding.Default.GetString(File.ReadAllBytes(filename));
        }

        public void saveFile(string filename, string data)
        {          
            //TO BASE64!!!!
            byte[] bData = Convert.FromBase64String(data);
            File.WriteAllBytes(filename, bData);
        }

        public void deleteFile(string filename)
        {
            System.IO.File.Delete(filename);
        }

        public List<string> listAllFilesInCategoryFolders(List<string> allPaths)
        {
            List<String> allFiles = new List<string>();
            // Get list of files in the specific directory.
            // ... Please change the first argument.
            for (int i = 0; i < allPaths.Count; i++)
            {
                string[] files = Directory.GetFiles(allPaths[i],
                    "*.*",
                    SearchOption.AllDirectories);

                // Display all the files.
                foreach (string file in files)
                {
                    allFiles.Add(file);
                }
            }
            return allFiles;
        }

    }
}
