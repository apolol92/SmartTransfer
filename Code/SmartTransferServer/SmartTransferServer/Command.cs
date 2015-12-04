using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTransferServer
{

    class Command
    {
        int id;
        string username;
        int typ;
        string filename;
        string parameter;
        string data;


        public string toString()
        {
            return "{" + id + ";" + username + ";" + typ + ";" + filename + ";" + parameter + ";" + data + "}";
        }

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

        public string Data
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
    }
}
