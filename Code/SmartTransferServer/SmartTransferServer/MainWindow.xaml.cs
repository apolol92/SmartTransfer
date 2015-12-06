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

namespace SmartTransferServer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            XmlManager xmlManager = new XmlManager();
            xmlManager.addChildToCategory(Categories.IMAGES, "C:/Users/gross/Pictures");
            xmlManager.addChildToCategory(Categories.MUSIC, "C:/Users/gross/Music");
            xmlManager.addChildToCategory(Categories.VIDEOS, "C:/Users/gross/Videos");
            xmlManager.saveXml();
            BroadcastSender mBroadcastSender = new BroadcastSender(1);
            Thread BcSender = new Thread(mBroadcastSender.run);
            BcSender.Start();
            DServer MyDServer = new DServer();
            Thread dservThread = new Thread(MyDServer.run);
            dservThread.Start();
            InitializeComponent();
        }
    }
}
