using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SunClounds.ViewModel
{
    public class CommandForBtn : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get { return _closeCommand; }
            set
            {
                _closeCommand = value;
                OnPropertyChanged(nameof(CloseCommand));
            }
        }

        private ICommand _maximizeCommand;
        public ICommand MaximizeCommand
        {
            get { return _maximizeCommand; }
            set
            {
                _maximizeCommand = value;
                OnPropertyChanged(nameof(MaximizeCommand));
            }
        }

        private ICommand _minimizeCommand;
        public ICommand MinimizeCommand
        {
            get { return _minimizeCommand; }
            set
            {
                _minimizeCommand = value;
                OnPropertyChanged(nameof(MinimizeCommand));
            }
        }

        public static CommandForBtn SharedCommand { get; } = new CommandForBtn();

        private CommandForBtn()
        {
            CloseCommand = new RelayCommand<object>(CloseButton_Click);
            MaximizeCommand = new RelayCommand<object>(MaximizeButton_Click);
            MinimizeCommand = new RelayCommand<object>(MinimizeButton_Click);
        }

        private void CloseButton_Click(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }

        private void MaximizeButton_Click(object parameter)
        {
            if (parameter is Window window)
            {
                if (window.WindowState == WindowState.Maximized)
                    window.WindowState = WindowState.Normal;
                else
                    window.WindowState = WindowState.Maximized;
            }
        }

        private void MinimizeButton_Click(object parameter)
        {
            if (parameter is Window window)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
