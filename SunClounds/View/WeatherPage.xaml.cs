using SunClounds.ViewModel;
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
using ThemeLib;

namespace SunClounds.View
{
    /// <summary>
    /// Логика взаимодействия для WeatherPage.xaml
    /// </summary>
    public partial class WeatherPage : Page
    {
        public WeatherPage(string cityName, string feelsLike, string minTemperature, string maxTemperature, string pressure, string windSpeed, string windDegree, string humidity)
        {
            InitializeComponent();

            feelsLikeTextBox.Text = $"Ощущение\n{feelsLike}";
            minTemperatureTextBlock.Text = $"Мин\n{minTemperature}";
            maxTemperatureTextBlock.Text = $"Макс\n{maxTemperature}";
            humidityTextBlock.Text = $"Влажность\n{humidity}";
            pressureTextBlock.Text = $"Давление\n{pressure} мм. рт. ст";
            windSpeedTextBlock.Text = $"Ветер м/c\n{windSpeed}";
            windDegreeTextBlock.Text = $"Ветер °\n{windDegree}";
            DataContext = new WeatherViewModel(cityName);
        }

    }
}
