using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartTransferServer_V2._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTransferServer_V2._0.Tests
{
    //[TestClass()]
    //public class SmartTransferServerTests
    //{
    //    public string SERVER_PW = "test123456789123";
    //    public string cmdStr = "{42;Hans;10;abc;none;none}";
    //    public string wrongCmdStr = "{42;10;abc;none;none}";

    //    [TestMethod()]
    //    public void testCommandExtraction()
    //    {
    //        CommandFactory cmdFactory = new CommandFactory();
    //        Command cmd = cmdFactory.extractCommandFromStr(cmdStr);
    //        Assert.AreEqual(cmdStr, cmd.toString());     
    //    }

    //    [TestMethod()]
    //    public void testWrongCommandExtraction()
    //    {
    //        CommandFactory cmdFactory = new CommandFactory();
    //        Command cmd = cmdFactory.extractCommandFromStr(wrongCmdStr);
    //        Assert.AreEqual(null, cmd);
    //    }

    //    [TestMethod()]
    //    public void testEncryptionDecryption()
    //    {
    //        CommandFactory cmdFactory = new CommandFactory();
    //        Command cmd = cmdFactory.extractCommandFromStr(cmdStr);
    //        Encrypter encrypter = new Encrypter(SERVER_PW);
    //        string encryptedStr = encrypter.encrypt(cmd);
    //        Decrypter decrypter = new Decrypter(SERVER_PW);
    //        string decryptedStr = decrypter.decrypt(Encoding.Default.GetBytes(encryptedStr));         
    //        Assert.AreEqual(cmdStr, decryptedStr);
    //    }

    //    [TestMethod()]
    //    public void testWrongPassword()
    //    {
    //        CommandFactory cmdFactory = new CommandFactory();
    //        Command cmd = cmdFactory.extractCommandFromStr(cmdStr);
    //        Encrypter encrypter = new Encrypter("lol1231254125123123123");
    //        string encryptedStr = encrypter.encrypt(cmd);
    //        Decrypter decrypter = new Decrypter(SERVER_PW);
    //        string decryptedStr = decrypter.decrypt(Encoding.Default.GetBytes(encryptedStr));
    //        Assert.AreEqual(Decrypter.WRONG_PASSWORD, decryptedStr);
    //    }
    //    [TestMethod()]
    //    public void testKillerNoKill()
    //    {
    //        CommandFactory cmdFactory = new CommandFactory();
    //        Command cmd = cmdFactory.extractCommandFromStr(cmdStr);
    //        Authenticator mAuthenicator = new Authenticator();
    //        Killer mKiller = new Killer(mAuthenicator);
    //        mKiller.LastAlive = mKiller.GetCurrentUnixTimestampMillis();
    //        Assert.AreEqual(false,mKiller.kill(cmd));         
    //    }
    //    [TestMethod()]
    //    public void testKillerKill()
    //    {
    //        CommandFactory cmdFactory = new CommandFactory();
    //        Command cmd = cmdFactory.extractCommandFromStr(cmdStr);
    //        Authenticator mAuthenicator = new Authenticator();
    //        Killer mKiller = new Killer(mAuthenicator);
    //        mKiller.LastAlive = mKiller.GetCurrentUnixTimestampMillis()+Killer.LIVING_TIME+1;
    //        Assert.AreEqual(true, mKiller.kill(cmd));
    //    }
    //    [TestMethod()]
    //    public void testKillerEntreation()
    //    {
    //        CommandFactory cmdFactory = new CommandFactory();
    //        Command cmd = cmdFactory.extractCommandFromStr("{42;Hans;3;none;none;none}");
    //        Authenticator mAuthenicator = new Authenticator();
    //        Killer mKiller = new Killer(mAuthenicator);
    //        mKiller.LastAlive = mKiller.GetCurrentUnixTimestampMillis()-20;
    //        mKiller.clientEntreation(cmd);
    //        Assert.AreEqual(mKiller.GetCurrentUnixTimestampMillis(), mKiller.LastAlive);
    //    }
    //    [TestMethod()]
    //    public void testAuthenticatorIsLoginFalse()
    //    {
    //        CommandFactory cmdFactory = new CommandFactory();
    //        Command cmd = cmdFactory.extractCommandFromStr("{42;Hans;10;abc;none;none}");
    //        Authenticator a = new Authenticator();
    //        a.Id = 10;
    //        a.Login = false;
    //        Assert.AreEqual(false, a.isLogin());
    //    }
    //    [TestMethod()]
    //    public void testAuthenticatorCorrectId()
    //    {
    //        CommandFactory cmdFactory = new CommandFactory();
    //        Command cmd = cmdFactory.extractCommandFromStr("{42;Hans;10;abc;none;none}");
    //        Authenticator a = new Authenticator();
    //        a.Id = 42;
    //        a.Login = true;
    //        Assert.AreEqual(true, a.isCorrectId(cmd));
    //    }

    //    [TestMethod()]
    //    public void testAuthenticatorFalseId()
    //    {
    //        CommandFactory cmdFactory = new CommandFactory();
    //        Command cmd = cmdFactory.extractCommandFromStr("{42;Hans;10;abc;none;none}");
    //        Authenticator a = new Authenticator();
    //        a.Id = 43;
    //        a.Login = true;
    //        Assert.AreEqual(false, a.isCorrectId(cmd));
    //    }

    //    [TestMethod()]
    //    public void testAuthenticatorisNoLoginCommand()
    //    {
    //        CommandFactory cmdFactory = new CommandFactory();
    //        Command cmd = cmdFactory.extractCommandFromStr("{42;Hans;10;abc;none;none}");
    //        Authenticator a = new Authenticator();
    //        a.Id = 42;
    //        a.Login = true;
    //        Assert.AreEqual(true, a.isNoLoginCommand(cmd));
    //    }
    //    [TestMethod()]
    //    public void testAuthenticatorisLoginCommand()
    //    {
    //        CommandFactory cmdFactory = new CommandFactory();
    //        Command cmd = cmdFactory.extractCommandFromStr("{-1;Hans;9;none;SERVER_PW;none}");
    //        Authenticator a = new Authenticator();
    //        a.Id = -1;
    //        a.Login = true;
    //        Assert.AreEqual(false, a.isNoLoginCommand(cmd));
    //    }
    //}
}