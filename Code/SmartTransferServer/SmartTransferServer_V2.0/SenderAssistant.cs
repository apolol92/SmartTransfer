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

        public void sendString(string str, Socket client)
        {
            byte[] data = Encoding.ASCII.GetBytes(str);
            client.Send(data, data.Length, SocketFlags.None);
        }

        internal void sendWrongPassword(Socket currentClient)
        {
            sendString(WRONG_PASSWORD, currentClient);
        }

        
        internal void sendWrongCommandFormat(Socket currentClient)
        {
            sendString(WRONG_COMMAND_FORMAT, currentClient);
        }

        internal void sendObituary(Socket currentClient)
        {
            sendString(KILLED_BY_KILLER, currentClient);
        }

        internal void sendLoginRequired(Socket currentClient)
        {
            sendString(LOGIN_REQUIRED, currentClient);
        }

        internal void sendLoginSucceed(Socket currentClient)
        {
            sendString(LOGIN_SUCCESSED, currentClient);
        }

        internal void sendWrongId(Socket currentClient)
        {
            sendString(WRONG_ID, currentClient);
        }
    }
}