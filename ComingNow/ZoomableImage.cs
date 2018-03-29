using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace ComingNow
{
    public class ZoomableImage : Image
    {
        private TransformGroup transformGroup = new TransformGroup();

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.None)
            {
                ChangeScale(e.GetPosition(this), e.Delta);
            }
            base.OnMouseWheel(e);
        }

        private void ChangeScale(Point center, int delta)
        {
            double scale = (0 < delta) ? 1.1 : (1.0 / 1.1);
            transformGroup.Children.Add(new ScaleTransform(scale, scale, center.X, center.Y));
            this.LayoutTransform = transformGroup;
        }
    }
}
