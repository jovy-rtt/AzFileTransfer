using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
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

namespace AzFTServer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceHost host;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            ChangeState(btnStart, false, btnStop, true);
            host = new ServiceHost(typeof(Service1));
            host.Open();
            string a = RuntimeInformation.FrameworkDescription;
            //textBlock1.Text = a;
            textBlock1.Text += "本机服务已启动，监听的Uri为：\n";
            foreach (var v in host.Description.Endpoints)
            {
                textBlock1.Text += v.ListenUri.ToString() + "\n";
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            host.Close();
            textBlock1.Text += "本机服务已关闭\n";
            ChangeState(btnStart, true, btnStop, false);
        }

        private static void ChangeState(Button btnStart, bool isStart, Button btnStop, bool isStop)
        {
            btnStart.IsEnabled = isStart;
            btnStop.IsEnabled = isStop;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (host != null)
            {
                if (host.State == CommunicationState.Opened)
                {
                    host.Close();
                }
            }
        }
    }
}
