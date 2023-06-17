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

namespace ThemeLib
{
    public static class ThemeManager
    {
        public static void SetThemeBasedOnTime()
        {
            DateTime currentTime = DateTime.Now;
            string themeName = "Day";
            ImageSource backgroundImage = new BitmapImage(new Uri("pack://application:,,,/ThemeLib;component/Themes/Day.png"));

            if (currentTime.Hour >= 0 && currentTime.Hour <= 3)
            {
                themeName = "Night";
                backgroundImage = new BitmapImage(new Uri("pack://application:,,,/ThemeLib;component/Themes/Night.png"));
            }
            else if (currentTime.Hour >= 4 && currentTime.Hour <= 11)
            {
                themeName = "MorningEvening";
                backgroundImage = new BitmapImage(new Uri("pack://application:,,,/ThemeLib;component/Themes/MorningEvening.png"));
            }


            SetTheme(themeName);
            SetBackgroundImage(backgroundImage);
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
