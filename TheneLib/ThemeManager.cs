using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace ThemeLib
{
    public static class ThemeManager
    {
        private static DispatcherTimer timer;

        public static void SetThemeBasedOnTime()
        {
            DateTime currentTime = DateTime.Now;
            string themeName = "Day";
            ImageSource backgroundImage = new BitmapImage(new Uri("pack://application:,,,/ThemeLib;component/Themes/Day.png"));

            if (currentTime.Hour >= 0 && currentTime.Hour <= 3)
            {
                themeName = "Night";
                backgroundImage = new BitmapImage(new Uri("pack://application:,,,/ThemeLib;component/Themes/Night.png"));
                SetTheme(themeName);
                SetBackgroundImage(backgroundImage);
            }
            else if (currentTime.Hour >= 16 && currentTime.Hour <= 23)
            {
                themeName = "MorningEvening";
                backgroundImage = new BitmapImage(new Uri("pack://application:,,,/ThemeLib;component/Themes/MorningEvening.png"));
                SetTheme(themeName);
                SetBackgroundImage(backgroundImage);
            }
            themeName = "Day";
            backgroundImage = new BitmapImage(new Uri("pack://application:,,,/ThemeLib;component/Themes/Day.png"));
            SetTheme(themeName);
            SetBackgroundImage(backgroundImage);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1); 
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            SetThemeBasedOnTime();
        }

        private static void SetTheme(string themeName)
        {
            if (Application.Current.Resources.MergedDictionaries.Count > 0)
            {
                Application.Current.Resources.MergedDictionaries.Clear();
            }

            ResourceDictionary themeDictionary = new ResourceDictionary();
            themeDictionary.Source = new Uri($"/ThemeLib;component/Themes/{themeName}.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(themeDictionary);
        }

        private static void SetBackgroundImage(ImageSource backgroundImage)
        {
            if (Application.Current.MainWindow != null)
            {
                ImageBrush backgroundBrush = new ImageBrush();
                backgroundBrush.ImageSource = backgroundImage;
                Application.Current.MainWindow.Background = backgroundBrush;
            }
        }
    }

}
