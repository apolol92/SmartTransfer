using System;
using System.Net.Sockets;
using System.Text;

namespace SmartTransferServer_V2._0
{
    public class SenderAssistant
    {
        const string WRONG_PASSWORD = "wrong password";
        const string WRONG_COMMAND_FORMAT = "wrong command format";
        const string KILLED_BY_KILLER = "killed";
        const string LOGIN_REQUIRED = "login required";
        const string LOGIN_SUCCESSED = "login succeed";
        const string WRONG_ID = "wrong id";

        public SenderAssistant()
        {
        }

        public void send(byte[] data, Socket client)
        {          
            client.Send(data, data.Length, SocketFlags.None);
        }

        
    }
}