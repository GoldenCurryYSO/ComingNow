using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using MastodonClient;

namespace ComingNow
{
    /// <summary>
    /// AuthorizationWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private string host;
        public AuthorizationWindow(string host)
        {
            InitializeComponent();
            this.host = host;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(await Client.GetAccessToken(host, this.Code.Text))
            {
                this.Close();
            }
            else
            {
                this.Code.Text = "";
            }
        }
    }
}
