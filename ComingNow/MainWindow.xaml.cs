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
        public MainWindow()
        {
            InitializeComponent();

            /*
            this.MouseLeftButtonDown += (sender, e) =>
            {
                this.DragMove();
            };
            
            this.CloseButton.Click += (sender, e) =>
            {
                this.Close();
            };
            */

            Client client = new Client(new GUIUpdater(this.Toots, App.Current.Dispatcher));
            client.GetLocalTimeline();
            client.StreamingLocalTimeline();
        }

        
    }
}
