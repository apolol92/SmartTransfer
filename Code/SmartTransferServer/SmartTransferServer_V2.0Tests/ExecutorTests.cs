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
        public void testGetAvaibleFilesRecursive()
        {
            //Have to do..
            XmlManager xmlManager = new XmlManager();
            //xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Music");
            xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Pictures\\test3");
            //TODO: Initialization
            xmlManager.addServerPassword("test123");
            xmlManager.saveXml();
            Killer mKiller = new Killer();
            Authenticator mAuthenticator = new Authenticator();
            CommandFactory cmdFactory = new CommandFactory();
            //------
            Command cmd = cmdFactory.extractCommandFromStr("{-1;Hans;4;none;none;none}");
            Executor mExecutor = new Executor(mAuthenticator, mKiller);
            cmd = mExecutor.execute(cmd);
            xmlManager.deleteXml();            
            Assert.AreEqual("C:\\Users\\Dennis\\Pictures\\test3\\test.odp+C:\\Users\\Dennis\\Pictures\\test3\\lol.odg+C:\\Users\\Dennis\\Pictures\\test3\\unterordner\\buum.txt+C:\\Users\\Dennis\\Pictures\\test3\\unterordner\\ka\\hier.txt", cmd.Parameter);
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
            xmlManager.deleteXml();
            //System.IO.File.Delete("C:\\Users\\Dennis\\Pictures\\test\\abc");
            
            Assert.AreEqual("saved file", cmd.Parameter);
          
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
            xmlManager.deleteXml();
            
            Assert.AreEqual("deleted file", cmd.Parameter);
           
        }

        [TestMethod()]
        public void testGetDataFromServer()
        {
            //Have to do..
            XmlManager xmlManager = new XmlManager();
            //xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Music");
            xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Pictures\\test2");
            //TODO: Initialization
            xmlManager.addServerPassword("test123");
            xmlManager.saveXml();
            Killer mKiller = new Killer();
            Authenticator mAuthenticator = new Authenticator();
            CommandFactory cmdFactory = new CommandFactory();
            //------
            Command cmd = cmdFactory.extractCommandFromStr("{42;Hans;0;C:\\Users\\Dennis\\Pictures\\test2\\abc.txt;none;none}");
            Executor mExecutor = new Executor(mAuthenticator, mKiller);
            cmd = mExecutor.execute(cmd);
            xmlManager.deleteXml();
           
            Assert.AreEqual("affe banane clown", cmd.Data);
            
        }

        [TestMethod()]
        public void testLogoutFromServer()
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
            mAuthenticator.Id = 42;
            mAuthenticator.Login = true;
            CommandFactory cmdFactory = new CommandFactory();
            //------
            Command cmd = cmdFactory.extractCommandFromStr("{42;Hans;8;none;none;none}");
            Executor mExecutor = new Executor(mAuthenticator, mKiller);
            cmd = mExecutor.execute(cmd);
            xmlManager.deleteXml();
           
            Assert.AreEqual("{-1;SERVER;7;none;OK;none}", cmd.toString());
            
        }

        [TestMethod()] 
        public void testErrorMsg()
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
            mAuthenticator.Id = 42;
            mAuthenticator.Login = true;
            CommandFactory cmdFactory = new CommandFactory();
            //------
            Command cmd = cmdFactory.extractCommandFromStr("{-1;SERVER;14;none;undefined error;none}");
            Executor mExecutor = new Executor(mAuthenticator, mKiller);
            cmd = mExecutor.execute(cmd);
            xmlManager.deleteXml();
            
            Assert.AreEqual("{-1;SERVER;7;none;undefined error;none}", cmd.toString());
            
        }

        [TestMethod()] 
        public void testThumbnail()
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
            mAuthenticator.Id = 42;
            mAuthenticator.Login = true;
            CommandFactory cmdFactory = new CommandFactory();            
            //------
            Command cmd = cmdFactory.extractCommandFromStr("{42;Hans;10;C:\\Users\\Dennis\\Pictures\\test\\würfel.png;none;none}");
            Executor mExecutor = new Executor(mAuthenticator, mKiller);
            cmd = mExecutor.execute(cmd);
            xmlManager.deleteXml();
            
            Assert.AreEqual(11, cmd.Typ);
           
        }
    }
}