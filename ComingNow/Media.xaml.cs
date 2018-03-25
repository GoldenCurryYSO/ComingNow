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

namespace ComingNow
{
    /// <summary>
    /// Media.xaml の相互作用ロジック
    /// </summary>
    public partial class Media : UserControl
    {
        //メディアのサイズの縦/横比
        const double propotion = 0.75;

        //メディアの横幅の全体に対する比
        const double width_rate = 0.75;

        public Media()
        {
            InitializeComponent();
        }
        
        public void AddMedia(BitmapSource bitmap)
        {
            System.Windows.Int32Rect rect;
            if (bitmap.PixelHeight > bitmap.PixelWidth * propotion)
            {
                rect = new System.Windows.Int32Rect(
                    0, (int)((bitmap.PixelHeight - bitmap.PixelWidth * propotion) / 2),
                    bitmap.PixelWidth, (int)(bitmap.PixelWidth * propotion)
                    );
            }
            else
            {
                rect = new System.Windows.Int32Rect(
                    (int)((bitmap.PixelHeight / propotion - bitmap.PixelHeight) / 2), 0,
                    (int)(bitmap.PixelHeight / propotion), bitmap.PixelHeight
                    );
            }
            var cropped = new CroppedBitmap(bitmap, rect);

            int count = this.MediaPanel.Children.Count;
            int column = count % 2;
            int row = count / 2;

            Image imageControl = new Image();
            imageControl.Source = cropped;
            Grid.SetColumn(imageControl, column);
            Grid.SetRow(imageControl, row);
            
            this.MediaPanel.Children.Add(imageControl);
        }
    }
}
