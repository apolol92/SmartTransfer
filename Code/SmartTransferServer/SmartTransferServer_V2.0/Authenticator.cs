using System;

namespace SmartTransferServer_V2._0
{
    public class Authenticator
    {
        const int LOGIN_TYP = 9;
        public static readonly int NOT_LOGGED_IN = -1;
        public bool Login { get; set; }
        public int Id { get; set; }

        public bool isNoLoginCommand(Command requestCommand)
        {
            return requestCommand.Typ != LOGIN_TYP;
        }

        public bool isLogin()
        {
            return Login;
        }

        public bool isCorrectId(Command requestCommand)
        {
            return requestCommand.Id == Id;
        }

        public bool isNoLoginCommand()
        {
            return this.Id == NOT_LOGGED_IN;
        }

        public int generateNewId()
        {
            Random rnd = new Random();
            int rndNum = rnd.Next();
            this.Id = rndNum;
            this.Login = true;
            return rndNum;
        }

        public void logout()
        {
            this.Login = false;
            this.Id = NOT_LOGGED_IN;
        }
    }
}