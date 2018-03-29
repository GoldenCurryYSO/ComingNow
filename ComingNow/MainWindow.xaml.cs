using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using MastodonClient;

namespace ComingNow
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private string host = "imastodon.net";

        public MainWindow()
        {
            InitializeComponent();
            
            this.Columns.Children.Add(new Column(host));
        }

        public void LogInButtonOnClick(Object sender, RoutedEventArgs e)
        {
            Client.Authorization(host);
            var authWin = new AuthorizationWindow(host);
            authWin.Show();
            authWin.Closed += (obj, eve) =>
            {
                (new MainWindow()).Show();
                this.Close();
            };
        }
    }
}
