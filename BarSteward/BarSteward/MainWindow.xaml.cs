using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Emgu.CV;
using Emgu.CV.Structure;

namespace BarSteward
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly DispatcherTimer _timer;
        private readonly VideoCapture _capture;

        public MainWindow()
        {
            InitializeComponent();
            _capture = new VideoCapture();

            //framerate of 10fps
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            _timer.Tick += async (object s, EventArgs a) =>
            {
                //draw the image obtained from camera
                using (Mat frame = _capture.QueryFrame())
                {
                    if (frame != null)
                    {
                        using (Image<Bgr, byte> image = frame.ToImage<Bgr, byte>())
                        {
                            CurrentFrame = image.ToBitmap();
                            imageCtrl.Source = Convert(CurrentFrame);
                        }
                    }
                }
            };
            _timer.Start();
        }
        private Bitmap _currentFrame;
        public Bitmap CurrentFrame
        {
            get => _currentFrame;
            private set
            {
                if (_currentFrame != value)
                {
                    _currentFrame?.Dispose();
                    _currentFrame = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private BitmapImage Convert(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
