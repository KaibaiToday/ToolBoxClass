using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ToolBox.ViewModel;

namespace ToolBox.Resources
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
    class BackgroundImage : ViewModelBase
    {
        public BackgroundImage()
        {
            try                 //使用trycatch防止返回“找不到bg.jpg”错误导致设计页面显示不正常
            {
                BitmapImage image = new BitmapImage(new Uri("./Resources/bg.png",UriKind.Relative));
                image.DecodePixelWidth = 1080;
                image.Freeze();
                _currentImage = image;
                CurrentImage = image;
            }
            catch
            {

            }
        }
        private BitmapImage _currentImage;
        public BitmapImage CurrentImage
        {
            get
            {
                return _currentImage;
            }
            set
            {
                _currentImage = value;
                OnPropertyChanged();
            }
        }
    }
}
