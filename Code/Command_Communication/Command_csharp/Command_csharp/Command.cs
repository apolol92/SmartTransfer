using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_csharp
{
    class Command
    {
        int id;
        string username;
        int typ;
        string filename;
        string parameter;
        byte[] data;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public int Typ
        {
            get
            {
                return typ;
            }

            set
            {
                typ = value;
            }
        }

        public string Filename
        {
            get
            {
                return filename;
            }

            set
            {
                filename = value;
            }
        }

        public string Parameter
        {
            get
            {
                return parameter;
            }

            set
            {
                parameter = value;
            }
        }

        public byte[] Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public string printInnerHeader()
        {
            return id + ";" + username + ";" + typ + ";" + filename + ";" + parameter + ";" + data;
        }

        public byte[] toByteArr()
        {
            string b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(printInnerHeader()));
            string header = "{" + b64 + "}";
            int len = header.Length + this.data.Length;
            byte[] rawCmd = new byte[len];
            for(int i=0;i< len;i++)
            {
                if (i < header.Length)
                {
                    rawCmd[i] = (byte)header[i];
                }
                else
                {
                    rawCmd[i] = (byte)this.data[i-header.Length];
                }
            }
            return rawCmd;
        }

        
    }
}
