using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace HMDColorFixerVR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainController _controller = new MainController();
        public MainWindow()
        {
            InitializeComponent();
            Title = Properties.Resources.AppName;
        }

        private void SliderR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _controller.SetColorValue(MainController.Color.R, (float)e.NewValue / 100);
        }

        private void SliderG_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _controller.SetColorValue(MainController.Color.G, (float)e.NewValue / 100);
        }

        private void SliderB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _controller.SetColorValue(MainController.Color.B, (float)e.NewValue / 100);
        }
    }
}
