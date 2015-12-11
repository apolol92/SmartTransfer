using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartTransferServer_V2._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTransferServer_V2._0.Tests
{
    [TestClass()]
    public class ExecutorTests
    {
        [TestMethod()]
        public void testGetAvaibleFiles()
        {
            //Have to do..
            XmlManager xmlManager = new XmlManager();
            //xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Music");
            xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Pictures\\test");
            //TODO: Initialization
            xmlManager.addServerPassword("test123");           
            xmlManager.saveXml();
            Killer mKiller = new Killer();
            Authenticator mAuthenticator = new Authenticator();            
            CommandFactory cmdFactory = new  CommandFactory();
            //------
            Command cmd = cmdFactory.extractCommandFromStr("{-1;Hans;4;none;none;none}");
            Executor mExecutor = new Executor(mAuthenticator,mKiller);
            cmd = mExecutor.execute(cmd);
            Assert.AreEqual("C:\\Users\\Dennis\\Pictures\\test\\würfel.png", cmd.Parameter);
            xmlManager.deleteXml();
        }

        [TestMethod()]
        public void testSaveDataOnServer()
        {
            //Have to do..
            XmlManager xmlManager = new XmlManager();
            //xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Music");
            xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Pictures\\test");
            //TODO: Initialization
            xmlManager.addServerPassword("test123");
            xmlManager.saveXml();
            Killer mKiller = new Killer();
            Authenticator mAuthenticator = new Authenticator();
            CommandFactory cmdFactory = new CommandFactory();
            //------
            Command cmd = cmdFactory.extractCommandFromStr("{42;Hans;1;C:\\Users\\Dennis\\Pictures\\test\\abc;none;Inhalt}");
            Executor mExecutor = new Executor(mAuthenticator, mKiller);
            cmd = mExecutor.execute(cmd);
            Assert.AreEqual("saved file", cmd.Parameter);
            xmlManager.deleteXml();
            //System.IO.File.Delete("C:\\Users\\Dennis\\Pictures\\test\\abc");
        }

        [TestMethod()]
        public void testDeleteDataFromServer()
        {
            //Have to do..
            XmlManager xmlManager = new XmlManager();
            //xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Music");
            xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Pictures\\test");
            //TODO: Initialization
            xmlManager.addServerPassword("test123");
            xmlManager.saveXml();
            Killer mKiller = new Killer();
            Authenticator mAuthenticator = new Authenticator();
            CommandFactory cmdFactory = new CommandFactory();
            //------
            Command cmd = cmdFactory.extractCommandFromStr("{42;Hans;2;C:\\Users\\Dennis\\Pictures\\test\\abc;none;none}");
            Executor mExecutor = new Executor(mAuthenticator, mKiller);
            cmd = mExecutor.execute(cmd);
            Assert.AreEqual("deleted file", cmd.Parameter);
            xmlManager.deleteXml();           
        }
    }
}