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
        public static string SERVER_PW;

        public SmartTransferServer()
        {
            XmlManager xmlManager = new XmlManager();
            SERVER_PW = xmlManager.readServerPassword();         
            
        }

        private string readPassword()
        {
            try
            {
                XmlManager xmlManager = new XmlManager();
                return xmlManager.readServerPassword();
            }
            catch (Exception ex)
            {
                return SERVER_PW;
            }            
        }

        public void run()
        {            
            Cleaner SmartCleaner = new Cleaner();
            Receiver CmdReceiver = new Receiver();
            //Decrypter CmdDecrypter = new Decrypter(SERVER_PW);
            CommandFactory CmdFactory = new CommandFactory();            
            SenderAssistant SmartSenderAssistant = new SenderAssistant();
            Authenticator SmartAuthenticator = new Authenticator();
            Killer SmartKiller = new Killer(SmartAuthenticator);
            Executor SmartExecutor = new Executor(SmartAuthenticator, SmartKiller);
            //Encrypter CmdEncrypter = new Encrypter(SERVER_PW);
            Sender SmartSender = new Sender();
            while(true)
            {
                //Read password everytime.. if changing..
                SERVER_PW = readPassword();
                //Clean following things before next round..
                SmartCleaner.clean(CmdReceiver.CurrentClient);
                Logger.getReady();
                //Receive an encrypted RequestCommandStr
                byte[] encryptedRequestComand = CmdReceiver.waitForCommand();
                Logger.incomingCommand();
                //Decrypt the encrypted RequestCommand
                byte[] decryptedRequestComand = Crypto.Decrypt(encryptedRequestComand,SERVER_PW);
                //If the encrypted RequestCommandStr has got a wrong encryption..
                if(decryptedRequestComand== null)
                {
                    Logger.wrongPassword(" just wrong..");
                    SmartSenderAssistant.sendWrongPassword(CmdReceiver.CurrentClient);
                    continue;
                }
                Logger.correctPassword();
                //Extract RequestCommandStr as Command-Instance
                Command requestCommand = CommandFactory.extractCommand(decryptedRequestComand);              
                //If requestCommandStr has got the wrong format..
                if(requestCommand== null)
                {
                    Logger.wrongCmdFormat(" just wrong format..");
                    SmartSenderAssistant.sendWrongCommandFormat(CmdReceiver.CurrentClient);
                    continue;
                }
                Logger.correctCmdFormat();
                if(SmartKiller.clientEntreation(requestCommand)==1)
                {
                    continue;
                }                 
                //If userid too old..
                if(SmartKiller.kill(requestCommand))
                {
                    Logger.userKilled();
                    SmartSenderAssistant.sendObituary(CmdReceiver.CurrentClient);
                    SmartAuthenticator.Login = false;
                    continue;
                }
                Logger.killerForgiven();
                //If there wasn't any login before..
                if(!SmartAuthenticator.isLogin())
                {
                    Logger.noActiveUser();
                    //Client don't want to connect.. FUCK CLIENT!!
                    if (SmartAuthenticator.isNoLoginCommand(requestCommand))
                    {
                        Logger.isNoLoginCommand();
                        SmartSenderAssistant.sendLoginRequired(CmdReceiver.CurrentClient);
                        continue;
                    }
                    //Client want to connect! .. let him.. PW checked before..
                    else
                    {
                        SmartKiller.LastAlive = SmartKiller.GetCurrentUnixTimestampMillis();   
                        //SmartSenderAssistant.sendLoginSucceed(CmdReceiver.CurrentClient);
                        Command loginSucceedResponse = CmdFactory.createLoginSuceedCommand(SmartAuthenticator);
                        Logger.loginSucceed(SmartAuthenticator.Id);                        
                        SmartSender.send(loginSucceedResponse, CmdReceiver.CurrentClient);
                        continue;
                    }                                      
                }
                //If there was a login.. Check if the user has got the correct Session-ID.. if not do this if
                if (SmartAuthenticator.isCorrectId(requestCommand)==false)
                {
                    Logger.wrongId(requestCommand.Id);
                    SmartSenderAssistant.sendWrongId(CmdReceiver.CurrentClient);
                    continue;
                }
                Logger.correctId(requestCommand.Id);
                //All was allright.. now execute command
                Command responseCommand = SmartExecutor.execute(requestCommand);
                //Encrypt and send responseCommand to client
                SmartSender.send(responseCommand,CmdReceiver.CurrentClient);
                Logger.finishedCommand();
            }
        }
    }
}
