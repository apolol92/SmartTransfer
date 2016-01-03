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
        const string WRONG_ID = "wrong id";

        public SenderAssistant()
        {
        }

        public void send(byte[] data, Socket client)
        {
            client.Send(data, data.Length, SocketFlags.None);
        }

        public void sendWrongPassword(Socket socket) {
            socket.Send(CommandFactory.createCommand(-1, "SERVER", 7, "none", WRONG_PASSWORD, new byte[1]).toByteArr(), SocketFlags.None);
        }

        internal void sendWrongCommandFormat(Socket currentClient)
        {
            currentClient.Send(CommandFactory.createCommand(-1, "SERVER", 7, "none", WRONG_COMMAND_FORMAT, new byte[1]).toByteArr(), SocketFlags.None);
        }

        internal void sendObituary(Socket currentClient)
        {
            currentClient.Send(CommandFactory.createCommand(-1, "SERVER", 7, "none", KILLED_BY_KILLER, new byte[1]).toByteArr(), SocketFlags.None);
        }

        internal void sendLoginRequired(Socket currentClient)
        {
            currentClient.Send(CommandFactory.createCommand(-1, "SERVER", 7, "none", LOGIN_REQUIRED, new byte[1]).toByteArr(), SocketFlags.None);
        }

        internal void sendWrongId(Socket currentClient)
        {
            currentClient.Send(CommandFactory.createCommand(-1, "SERVER", 7, "none", WRONG_ID, new byte[1]).toByteArr(), SocketFlags.None);
        }
    }
}