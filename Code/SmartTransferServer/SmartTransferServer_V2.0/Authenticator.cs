using System;

namespace SmartTransferServer_V2._0
{
    internal class Authenticator
    {
        const int LOGIN_TYP = 9;

        public bool Login { get; internal set; }
        public int Id { get; internal set; }

        internal bool isNoLoginCommand(Command requestCommand)
        {
            return requestCommand.Typ != LOGIN_TYP;
        }

        internal bool isLogin()
        {
            return Login;
        }

        internal bool isCorrectId(Command requestCommand)
        {
            return requestCommand.Id == Id;
        }

        internal bool isNoLoginCommand()
        {
            throw new NotImplementedException();
        }
    }
}