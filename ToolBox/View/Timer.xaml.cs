using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToolBox.Resources;
using ToolBox.ViewModel;

namespace ToolBox.View
{
    /// <summary>
    /// Timer.xaml 的交互逻辑
    /// </summary>
    public partial class Timer : UserControl
    {
        ProgressBar progressBar = new ProgressBar();
        public Timer()
        {
            InitializeComponent();
            GC.Collect();
            Update();
            StartTimer();
            this.DataContext = progressBar;
            Ratio = 1;
        }

        private void StartTimer()
        {
            Thread thread = new Thread(() => {
                TimeSpan deltaTime = new TimeSpan(0, 5, 0);
                TimeSpan currentTime;
                TimeSpan otime = new TimeSpan(deltaTime.Hours + DateTime.Now.Hour, deltaTime.Minutes + DateTime.Now.Minute, deltaTime.Seconds + DateTime.Now.Second);
                double delta = 1 / (deltaTime.TotalSeconds*100);
                Ratio = 1;
                while (Ratio>0)
                {
                    currentTime = new TimeSpan(0,DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second,DateTime.Now.Millisecond);
                    currentTime = otime.Subtract(currentTime);
                    progressBar.time = currentTime.Hours.ToString()+":"+currentTime.Minutes.ToString()+":"+currentTime.Seconds.ToString();
                    Ratio = currentTime.TotalMilliseconds / deltaTime.TotalMilliseconds;
                }
            });
            thread.Start();

        }
        private void Update()
        {
            Thread thread = new Thread(()=>{
                while (true)
                {
                    progressBar.EndPoint = progressBar.GetPoint(Ratio, ((TimeBar.ActualWidth - 10) / 2));
                    try { 
                    progressBar.size = new Size(((double)TimeBar.ActualWidth * 0.5 - 5), ((double)TimeBar.ActualWidth * 0.5 - 5));
                    }
                    catch
                    {

                    }
                    Thread.Sleep(10);
                }
            });
            thread.Start();
            
        }
        private double _ratio;
        public double Ratio
        {
            get { return _ratio; }
            set { _ratio = value; }
        }
        private void progressbar_Changed(object sender, EventArgs e)
        {

        }
    }

    class ProgressBar : ViewModel.ViewModelBase
    {
        public ProgressBar()
        {

        }
        private Point _point = new Point(50,50);
        bool _IsLarge = true;
        public Point GetPoint(double ratio,double r)
        {
            Point point;
            if(ratio<=0.5)
            {
                if(ratio<0.25)
                {
                    IsLarge = false;
                    point = new Point((r + r * Math.Sin(2 * Math.PI * ratio)+5), (5 + r - (Math.Cos(2 * Math.PI * (1 - ratio)) * r))); // (5 + r - (Math.Cos(2 * 3.14 * (1 - ratio)) * r))
                }
                else
                {
                    IsLarge = false;
                    point = new Point((r + r * Math.Sin(2 * Math.PI * ratio)+5), (5 + r - (Math.Cos(2 * Math.PI * (1 - ratio)) * r))); // (5 + r - (Math.Cos(2 * 3.14 * (1 - ratio)) * r))
                }
            }
            else
            {
                if (ratio < 0.75)
                {
                    IsLarge = true;
                    point = new Point((r + r * Math.Sin(2 *Math.PI * ratio)+5), (5 + r - (Math.Cos(2 * Math.PI * (1 - ratio)) * r))); // (5 + r - (Math.Cos(2 * 3.14 * (1 - ratio)) * r))
                }
                else
                {
                    IsLarge = true;
                    point = new Point((r + r * Math.Sin(2 * Math.PI * ratio)+5), (5 + r - (Math.Cos(2 * Math.PI * (1 - ratio)) * r))); // (5 + r - (Math.Cos(2 * 3.14 * (1 - ratio)) * r))
                }
            }
            GC.Collect();
            return point;
        }

        public Point EndPoint
        {
            get { return _point; }
            set
            {
                _point = value;
                OnPropertyChanged();
            }
        }

        public bool IsLarge
        {
            get{
                return _IsLarge;
            }
            set
            {
                _IsLarge= value;
                OnPropertyChanged();
            }
        }
        private Size _size;
        public Size size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                OnPropertyChanged();
            }
        }
        private string _time;
        public string time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value; OnPropertyChanged();
            }
        }
    }
    public class EllipseSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value*0.5);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StartPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Point(((double)value * 0.5),5);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
