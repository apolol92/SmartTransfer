using System;

namespace SmartTransferServer_V2._0
{
    public class Authenticator
    {
        const int LOGIN_TYP = 9;

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
            throw new NotImplementedException();
        }
    }
}