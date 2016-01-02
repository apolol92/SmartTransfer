using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_csharp
{
    class FileManager
    {
        public static byte[] readFile()
        {
            return File.ReadAllBytes("wrfel.png");
        }

        public static void writeFile(byte[] data)
        {
            File.WriteAllBytes("incoming.png", data);
        }
    }
}
