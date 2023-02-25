using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace ToolBox.View
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : UserControl
    {
        bool IsStart = false;
        Stack NumberBeingPicked;
        struct KeyMan
        {
            public int Order;
            public int Number;
        }
        KeyMan keyman;

        public Home()
        {
            InitializeComponent();
            NumberBeingPicked = new Stack();
            int[] keys = ReadKeys();
            if (keys != null)
            {
                keyman.Order = keys[0];
                keyman.Number = keys[1];
            }
            GC.Collect();
        }

        private int[] ReadKeys()
        {
            int[] keys = new int[2];
            string keystring = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader("./Resources/range.sta"))
                {
                    tb_max.Text = sr.ReadToEnd();
                }
                using (StreamReader sr = new StreamReader("./a.keys"))
                {
                    keystring = sr.ReadToEnd();
                }
                string[] key = keystring.Split('_');
                keys[0] = int.Parse(key[0]);
                keys[1] = int.Parse(key[1]);
                File.Delete("./a.keys");
                return keys;
            }
            catch
            {
                return null;
            }
        }
        private void TimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsStart)
            {
                IsStart = false;
                TimerButton.Content = "开始";
            }
            else
            {
                TimerButton.Content = "停止";
                IsStart = true;
                StartPick(int.Parse(tb_max.Text));
            }
        }

        private void StartPick(int range)
        {
            range = range + 1; //random.next的参数范围是开区间
            Thread Pick = new Thread(() =>
            {
                Random randomseed = new Random();
                Random randomresult = new Random(randomseed.Next(1, range));
                int result = 0;
                while (IsStart)
                {
                    result = randomresult.Next(1, range);
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, () =>
                    {
                        if (result < 10)
                        {
                            label.Content = "0" + result.ToString();
                        }
                        else
                        {
                            label.Content = result.ToString();
                        }
                    });
                    Thread.Sleep(50);
                }
                NumberBeingPicked.Push(result);
                if (NumberBeingPicked.Count == keyman.Order)
                {
                    result = keyman.Number;
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, () =>
                    {
                        if (result < 10)
                        {
                            label.Content = "0" + result.ToString();
                        }
                        else
                        {
                            label.Content = result.ToString();
                        }
                    });
                }
            });
            Pick.Start();
        }

        private void tb_max_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("./Resources/range.sta"))
            {
                sw.Write(tb_max.Text);
            }
        }
    }
    public class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double size = (double)value / 6;
            return size;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double size = (double)value / 6;
            return size;
        }
    }

    public class TimeBarSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double size = (double)value * 0.7;
            return size;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double size = (double)value * 0.7;
            return size;
        }
    }
    public class ButtonFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double size = (double)value / 7;
            return size;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double size = (double)value / 7;
            return size;
        }
    }
    public class GridSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double size = (double)value * 0.3;
            return size;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double size = (double)value * 0.3;
            return size;
        }
    }
}
