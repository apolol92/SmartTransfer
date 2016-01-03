using System;
using System.Net.Sockets;

namespace SmartTransferServer_V2._0
{
    public class Sender
    {
        public Sender()
        {
        }


        internal void send(Command responseCommand, Socket currentClient)
        {
            SenderAssistant senderAssistent = new SenderAssistant();
            senderAssistent.send(Crypto.Encrypt(responseCommand.toByteArr(),SmartTransferServer.SERVER_PW), currentClient);            
            currentClient.Close();
        }
    }
}