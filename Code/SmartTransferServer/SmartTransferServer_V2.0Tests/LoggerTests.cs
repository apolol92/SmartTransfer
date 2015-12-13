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
    public class LoggerTests
    {
        [TestMethod()]
        public void testGetAvaibleFiles()
        {
            Logger logger = new Logger();
            logger.getAvaibleFiles();
            logger.getAvaibleFiles();
            logger.getAvaibleFiles();
            logger.getAvaibleFiles();
            logger.getAvaibleFiles();    
           


        }
    }
}