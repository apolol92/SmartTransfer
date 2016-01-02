using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartTransferServer_V2._0
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            XmlManager xmlDeleter = new XmlManager();
            xmlDeleter.deleteXml();
            InitializeComponent();
            XmlManager xmlManager = new XmlManager();
            xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Music");
            xmlManager.addChildToCategory(Categories.MUSIC, "C:\\Users\\Dennis\\Pictures");
            //TODO: Initialization
            xmlManager.addServerPassword("test123456789123");
            xmlManager.saveXml();
            BroadcastSender mBroadcastSender = new BroadcastSender(1);
            Thread BcSender = new Thread(mBroadcastSender.run);
            BcSender.Start();
            SmartTransferServer smartTransferServer = new SmartTransferServer();
            Thread serverThread = new Thread(smartTransferServer.run);
            serverThread.Start();
        }
    }
}
