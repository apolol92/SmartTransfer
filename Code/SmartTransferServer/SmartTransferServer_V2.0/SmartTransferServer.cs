using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTransferServer_V2._0
{
    public class SmartTransferServer
    {
        public static readonly int SERVER_PORT = 7000;
        public string SERVER_PW;
        public SmartTransferServer()
        {
            XmlManager xmlManager = new XmlManager();
            xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Music");
            xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Pictures");
            //TODO: Initialization
            xmlManager.addServerPassword("test123");
            SERVER_PW = xmlManager.readServerPassword();
            xmlManager.saveXml();
        }

        public void run()
        {
            Cleaner SmartCleaner = new Cleaner();
            Receiver CmdReceiver = new Receiver();
            Decrypter CmdDecrypter = new Decrypter(SERVER_PW);
            CommandFactory CmdFactory = new CommandFactory();
            Killer SmartKiller = new Killer();
            SenderAssistant SmartSenderAssistant = new SenderAssistant();
            Authenticator SmartAuthenticator = new Authenticator();
            Executor SmartExecutor = new Executor(SmartAuthenticator, SmartKiller);
            Encrypter CmdEncrypter = new Encrypter(SERVER_PW);
            Sender SmartSender = new Sender();
            while(true)
            {
                //Clean following things before next round..
                SmartCleaner.clean(CmdReceiver.CurrentClient);
                //Receive an encrypted RequestCommandStr
                String encryptedRequestComandStr = CmdReceiver.waitForRequestCommand();
                //Decrypt the encrypted RequestCommandStr
                String decryptedRequestComandStr = CmdDecrypter.decrypt(encryptedRequestComandStr);
                //If the encrypted RequestCommandStr has got a wrong encryption..
                if(decryptedRequestComandStr== Decrypter.WRONG_PASSWORD)
                {
                    SmartSenderAssistant.sendWrongPassword(CmdReceiver.CurrentClient);
                    continue;
                }
                //Extract RequestCommandStr as Command-Instance
                Command requestCommand = CmdFactory.extractCommandFromStr(decryptedRequestComandStr);
                //If requestCommandStr has got the wrong format..
                if(requestCommand== null)
                {
                    SmartSenderAssistant.sendWrongCommandFormat(CmdReceiver.CurrentClient);
                    continue;
                }
                SmartKiller.clientEntreation(requestCommand);
                //If userid too old..
                if(SmartKiller.kill(requestCommand))
                {
                    SmartSenderAssistant.sendObituary(CmdReceiver.CurrentClient);
                    SmartAuthenticator.Login = false;
                    continue;
                }
                //If there wasn't any login before..
                if(!SmartAuthenticator.isLogin())
                {
                    //Client don't want to connect.. FUCK CLIENT!!
                    if (SmartAuthenticator.isNoLoginCommand(requestCommand))
                    {
                        SmartSenderAssistant.sendLoginRequired(CmdReceiver.CurrentClient);
                        continue;
                    }
                    //Client want to connect! .. let him.. PW checked before..
                    else
                    {
                        //SmartSenderAssistant.sendLoginSucceed(CmdReceiver.CurrentClient);
                        Command loginSucceedResponse = CmdFactory.createLoginSuceedCommand(SmartAuthenticator);                        
                        SmartSender.send(loginSucceedResponse, CmdReceiver.CurrentClient,CmdEncrypter);
                        continue;
                    }                                      
                }
                //If there was a login.. Check if the user has got the correct Session-ID.. if not do this if
                if (SmartAuthenticator.isCorrectId(requestCommand)==false)
                {
                    SmartSenderAssistant.sendWrongId(CmdReceiver.CurrentClient);
                    continue;
                }
                //All was allright.. now execute command
                Command responseCommand = SmartExecutor.execute(requestCommand);
                //Encrypt and send responseCommand to client
                SmartSender.send(responseCommand,CmdReceiver.CurrentClient, CmdEncrypter);
            }
        }
    }
}
