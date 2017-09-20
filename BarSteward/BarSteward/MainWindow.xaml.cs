using System.Drawing;
using System.IO;
using System.Windows;
using BarSteward.Services;

namespace BarSteward
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();      
            this.DataContext = this;
        }
        

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Bitmap bitmap = camera.Capture();
            string base64 = Encode.Base64Encode(bitmap);
            File.WriteAllText(@"c:\image.txt", base64);
        }
    }
}
