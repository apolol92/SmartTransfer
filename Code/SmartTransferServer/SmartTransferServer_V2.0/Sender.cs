using System;
using System.Net.Sockets;

namespace SmartTransferServer_V2._0
{
    internal class Sender
    {
        public Sender()
        {
        }


        internal void send(Command responseCommand, Socket currentClient)
        {
            SenderAssistant senderAssistent = new SenderAssistant();
            senderAssistent.sendString(responseCommand.toString(), currentClient);
        }
    }
}