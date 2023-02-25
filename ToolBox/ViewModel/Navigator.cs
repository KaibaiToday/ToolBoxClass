using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToolBox.View;

namespace ToolBox.ViewModel
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
    class Navigator : ViewModelBase
    {
        public Navigator()
        {
            homepage = new Home();
            CurrentView = homepage;
            HomePageCommand = new RelayCommand(Home);
            TimerPageCommand = new RelayCommand(timer);
        }
        private object _currentView;
        private object? timerpage = null;
        private object? homepage = null;
        public object CurrentView
        {
            get
            {
                return _currentView;
            }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public ICommand HomePageCommand
        {
            get; set;
        }          //导航栏按钮点击事件
        public ICommand TimerPageCommand
        {
            get; set;
        }

        private void Home(object obj)
        {
            if (homepage == null)                       //防止重复初始化对象浪费内存
            {
                homepage = new Home();
                CurrentView = homepage;
            }
            else
            {
                CurrentView = homepage;
            }
        }
        private void timer(object obj)
        {
            if (timerpage == null)                       //防止重复初始化对象浪费内存
            {
                timerpage = new Timer();
                CurrentView = timerpage;
            }
            else
            {
                CurrentView = timerpage;
            }
        }
    }
    class RelayCommand : ICommand //用于处理command绑定
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
    }
}
