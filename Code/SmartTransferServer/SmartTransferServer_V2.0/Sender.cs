using System;
using System.Net.Sockets;

namespace SmartTransferServer_V2._0
{
    public class Sender
    {
        public Sender()
        {
        }


        internal void send(Command responseCommand, Socket currentClient, Encrypter mEncrypter)
        {
            SenderAssistant senderAssistent = new SenderAssistant();
            senderAssistent.send(mEncrypter.encrypt(responseCommand), currentClient);
            currentClient.Close();
        }
    }
}