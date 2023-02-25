using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToolBox;

namespace ToolBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rect gridrect = new Rect(0,0,0,0);
        public int length = 0;              //用于指定当前页面
        struct OriginalState
        {
            public double top;
            public double left;
            public double width;
            public double height;
        }
        Stack StateRecord = new Stack();
        bool IsMaximized= false;            //是否最大化（由于代码手动更改，所以和系统显示的不一样
        public MainWindow()
        {
            InitializeComponent();
            using (StreamReader sr = new StreamReader("./Resources/location.sta"))
            {
                string[] state = sr.ReadToEnd().Split('_');
                this.Top = int.Parse(state[0]);
                this.Left= int.Parse(state[1]);
                this.Width= int.Parse(state[2]);
                this.Height = int.Parse(state[3]);
            }
            OriginalState thistime = new OriginalState();       //记录最大化之前的窗体状态
            thistime.top = this.Top;
            thistime.left = this.Left;
            thistime.width = this.Width;
            thistime.height = this.Height;
            StateRecord.Push(thistime);
        }

        private void ChangeLocalState()
        {
            if (IsMaximized)
            {

            }
            else
            {
                using (StreamWriter sw = new StreamWriter("./Resources/location.sta"))
                {
                    sw.Write(this.Top + "_" + this.Left + "_" + this.Width + "_" + this.Height);
                }
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            gridrect.Width = this.ActualWidth;
            gridrect.Height = this.ActualHeight;
            this.Width = this.ActualWidth;              //微软的simabug，不加这两句会导致最大化时宽高不跟着变，gridrect大小溢出
            this.Height = this.ActualHeight;
            if(this.WindowState== WindowState.Normal)
            {
                if (this.ActualHeight == SystemParameters.WorkArea.Height || this.ActualWidth == SystemParameters.WorkArea.Width)   //防止在移动窗体使其最大化的过程中推入最大化的数据
                {
                    IsMaximized = true;
                }
                else
                {
                    OriginalState thistime = new OriginalState();       //记录最大化之前的窗体状态
                    thistime.top = this.Top;
                    thistime.left = this.Left;
                    thistime.width = this.Width;
                    thistime.height = this.Height;
                    if (StateRecord.Count >= 5)
                    {
                        var a = StateRecord.Pop();  //防止把当前状态给推了
                        StateRecord.Clear();
                        StateRecord.Push(a);
                    }
                    StateRecord.Push(thistime);
                }
            }
            GridRect.Rect = gridrect;                   //rect是结构体变量，无法使用绑定，故使用这种方式来更新视图
            ChangeLocalState();
        }

        private void ButtonBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                IsMaximized = true;
                if (this.WindowState == WindowState.Normal)
                {
                    OriginalState thistime = new OriginalState();       //记录最大化之前的窗体状态
                    thistime.top = this.Top;
                    thistime.left = this.Left;
                    thistime.width = this.Width;
                    thistime.height = this.Height;
                    if (StateRecord.Count >= 5)
                    {
                        var a = StateRecord.Pop();  //防止把当前状态给推了
                        StateRecord.Clear();
                        StateRecord.Push(a);
                    }
                    StateRecord.Push(thistime);
                }
                this.Top = 0;
                this.Left = 0;
                this.Width = SystemParameters.WorkArea.Size.Width;
                this.Height = SystemParameters.WorkArea.Size.Height;
            }
            else
            {
                //移动窗体
                Brush OnDragBrush = new SolidColorBrush(Color.FromArgb(230, 126, 165, 241));        //顶边框颜色变化
                background.Background = OnDragBrush;
                if (IsMaximized == true)        //从最大化恢复窗体状态
                {
                    this.WindowState = WindowState.Normal;
                    while (((OriginalState)StateRecord.Peek()).width == SystemParameters.WorkArea.Size.Width || ((OriginalState)StateRecord.Peek()).height == SystemParameters.WorkArea.Size.Height)
                    {
                         StateRecord.Pop();//推出堆栈顶端两个已经是最大化的状态（其实是上面把state改成了normal然后再调整宽高导致的两个废数据）
                    }
                    var originaltype = (OriginalState)StateRecord.Peek();
                    this.Top = Mouse.GetPosition(this).Y - 25;//窗体跟随鼠标，25和100只是为了让它比较好看，不影响跟随功能
                    this.Left = Mouse.GetPosition(this).X - 100;
                    this.Width = originaltype.width;
                    this.Height = originaltype.height;
                    IsMaximized = false;
                }
                try
                {
                    this.DragMove();
                }
                catch
                {

                }
                OnDragBrush = new SolidColorBrush(Color.FromArgb(159, 126, 165, 241));
                background.Background = OnDragBrush;
                //在移动后记录窗体位置
                if (this.WindowState == WindowState.Normal)
                {
                    OriginalState thistime = new OriginalState();       //记录最大化之前的窗体状态
                    thistime.top = this.Top;
                    thistime.left = this.Left;
                    thistime.width = this.Width;
                    thistime.height = this.Height;
                    if (StateRecord.Count >= 5)
                    {
                        var a = StateRecord.Pop();  //防止把当前状态给推了
                        StateRecord.Clear();
                        StateRecord.Push(a);
                    }
                    StateRecord.Push(thistime);
                }
            }
            ChangeLocalState();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {           //窗口关闭动画，完成后执行Environment.exit
            Storyboard CloseAnimation =(Storyboard)this.Resources["HideWindow"];
            ChangeLocalState();
            CloseAnimation.Completed +=  delegate{ Environment.Exit(0); };
            CloseAnimation.Begin(this);
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {           //窗口开启动画
            Storyboard CloseAnimation = (Storyboard)this.Resources["ShowWindow"];
            CloseAnimation.Begin(this);
        }

        private void window_StateChanged(object sender, EventArgs e)
        {
            this.Width= this.ActualWidth;
            this.Height= this.ActualHeight;
            if (this.WindowState == WindowState.Maximized)          //防止窗体溢出屏幕
            {
                IsMaximized = true;
                this.WindowState= WindowState.Normal;
                this.Top = 0;
                this.Left= 0;
                this.Width = SystemParameters.WorkArea.Size.Width;
                this.Height= SystemParameters.WorkArea.Size.Height;
            }
            GC.Collect();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsMaximized == true)
            {
                this.WindowState = WindowState.Normal;
                while (((OriginalState)StateRecord.Peek()).width == SystemParameters.WorkArea.Size.Width || ((OriginalState)StateRecord.Peek()).height == SystemParameters.WorkArea.Size.Height)
                {
                    StateRecord.Pop();//推出堆栈顶端两个已经是最大化的状态（其实是上面把state改成了normal然后再调整宽高导致的两个废数据）
                }
                var originaltype = (OriginalState)StateRecord.Peek();
                this.Top = originaltype.top;
                this.Left = originaltype.left;
                this.Width = originaltype.width;
                this.Height = originaltype.height;
                IsMaximized = false;
            }
            else
            {
                IsMaximized = true;
                if (this.WindowState == WindowState.Normal)
                {
                    OriginalState thistime = new OriginalState();       //记录最大化之前的窗体状态
                    thistime.top = this.Top;
                    thistime.left = this.Left;
                    thistime.width = this.Width;
                    thistime.height = this.Height;
                    if (StateRecord.Count >= 5)
                    {
                        var a = StateRecord.Pop();  //防止把当前状态给推了
                        StateRecord.Clear();
                        StateRecord.Push(a);
                    }
                    StateRecord.Push(thistime);
                }
                this.Top = 0;
                this.Left = 0;
                this.Width = SystemParameters.WorkArea.Size.Width;
                this.Height = SystemParameters.WorkArea.Size.Height;
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            //窗口关闭动画，完成后最小化
            Storyboard CloseAnimation = (Storyboard)this.Resources["HideWindow"];
            CloseAnimation.Completed += delegate { this.WindowState = WindowState.Minimized; };
            CloseAnimation.Begin(this);
        }
    }
    public class ContainerWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double width = (double)value - 50;
            return width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double width = (double)value - 50;
            return width;
        }
    }
    public class TitleWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double width = ((double)value-120) / 2;
            return width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double width = ((double)value - 120) / 2;
            return width;
        }
    }
    public class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double size = (double)value / 9;
            return size;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double size = (double)value / 9;
            return size;
        }
    }
}
