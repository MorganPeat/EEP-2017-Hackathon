using System;
using System.ComponentModel;
using System.Windows.Controls;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace BarSteward.Camera
{
    /// <summary>
    /// Interaction logic for ImageCapture.xaml
    /// </summary>
    public partial class ImageCapture : UserControl, INotifyPropertyChanged
    {
        private readonly DispatcherTimer _timer;
        private readonly VideoCapture _capture;

        public ImageCapture()
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
    }
}
