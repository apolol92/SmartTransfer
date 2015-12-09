using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SmartTransferServer
{
    /// <summary>
    /// This is the most important class in this project.
    /// With this class we will control and do the transfer between smartphone and PC.
    /// </summary>
    class DServer
    {
        /// <summary>
        /// This is the tcp-server
        /// </summary>
        TcpListener server = null;
        /// <summary>
        /// This ist the SERVER_PORT
        /// </summary>
        private const int SERVER_PORT = 1314;
        /// <summary>
        /// Initialize our server
        /// </summary>
        public DServer()
        {
           try
            {
                this.server = new TcpListener(SERVER_PORT);
                this.server.Start();
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// Extract the requestCommandStr
        /// </summary>
        /// <param name="client">A Reference of the client</param>
        /// <returns></returns>
        public String getRequestCommandStr(TcpClient client)
        {
            Byte[] bytes = new Byte[256];
            NetworkStream stream = client.GetStream();
            int i;
            String RequestCommandStr = "";
            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                RequestCommandStr += System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            }
            return RequestCommandStr;
        }
        /// <summary>
        /// This method sends the response of the request command
        /// </summary>
        /// <param name="ResponseCommand"></param>
        public void sendResponseCommand(Command ResponseCommand)
        {
            CommandFactory cmdFactory = new CommandFactory();
            //To be sure that cmd is really marked..
            ResponseCommand = cmdFactory.markAllSpecialCharacters(ResponseCommand);
            this.server.Server.Send(Encoding.ASCII.GetBytes(ResponseCommand.toString()));
        }

        private static int generateNewId(int lastId)
        {
            int nId = lastId;
            Random rnd = new Random();
            while (nId == lastId)
            {
                nId = rnd.Next(0, Int32.MaxValue);
            }
            return nId;
        }

        /// <summary>
        /// Use this method to run the server
        /// </summary>
        public void run()
        {
            //This Factory creates commands
            CommandFactory MyCommandFactory = new CommandFactory();
            //This Guardian protects the current user
            Guardian MyGuardian = new Guardian();
            //This Executor executes commands
            Executor MyExecutor = new Executor(MyGuardian);
            while (true)
            {
                //This Command will be the response command
                Command ResponseCommand;
                //Wait for a client
                TcpClient client = server.AcceptTcpClient();
                //Get RequestCommand as String
                String RequestCommandStr = getRequestCommandStr(client);
                //Extract RequestCommandStr
                Command CurrentCommand = MyCommandFactory.extractCommandFromStr(RequestCommandStr);
                //Wrong command?
                if(CurrentCommand == null)
                {
                    continue;
                }
                //Is no user under protection/login?
                if (!MyGuardian.isGuarding() && CurrentCommand.Typ ==9)
                {
                    //If Password correct.. Login
                    if (CurrentCommand.Parameter == MyGuardian.ServerPassword)
                    {
                        CurrentCommand.Id = MyGuardian.generateGuardingId();
                        //Activate keep alive
                        MyGuardian.keepClientAlive();
                    }
                    else
                    {
                        //TODO: Wrong PASSWORD!!
                    }
                }
                //No user is logged in, but no incoming login
                else if(!MyGuardian.isGuarding() && CurrentCommand.Typ != 9)
                {
                    //Continue if no login
                    continue;
                }
                //Is this the protected user & all alright?
                if(MyGuardian.getGuardingId()==CurrentCommand.Id && MyGuardian.isClientAlive())
                {
                    ResponseCommand = MyExecutor.execute(CurrentCommand);
                    //Generate new ID for security reasons
                    ResponseCommand.Id = generateNewId(CurrentCommand.Id);
                    //Let the new ID know by the guardian
                    MyGuardian.setGuardingId(ResponseCommand.Id);
                }
                //Client is dead and sent command to late
                else if(MyGuardian.getGuardingId() == CurrentCommand.Id && !MyGuardian.isClientAlive())
                {
                    CurrentCommand.Typ = 8; //Simulate User logout
                    //Kill Client..
                    ResponseCommand = MyExecutor.execute(CurrentCommand);
                }
                //Other user is connecting while main user is dead
                else if(!MyGuardian.isClientAlive())
                {
                    //Reset rest
                    MyGuardian.resetProtection();
                    //Generate new command
                    CurrentCommand.Id = MyGuardian.generateGuardingId();
                    //Activate keep alive
                    MyGuardian.keepClientAlive();
                    //Create Response with login successed
                    ResponseCommand = MyExecutor.execute(CurrentCommand);
                }
                else
                {
                    //No access, because other user is using the server
                    ResponseCommand = MyExecutor.createErrorCommand(CurrentCommand);
                }              
                //Send response             
                sendResponseCommand(ResponseCommand);
            }
        }
    }

   
}
