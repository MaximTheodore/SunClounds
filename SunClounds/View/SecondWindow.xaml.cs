﻿using SunClounds.ViewModel;
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
using System.Windows.Shapes;
using ThemeLib;

namespace SunClounds.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(string cityName, string feelsLike, string minTemperature, string maxTemperature, string pressure, string windSpeed, string windDegree, string humidity)
        {
            InitializeComponent();
            ThemeManager.SetThemeBasedOnTime();
            Page.Content = new WeatherPage(cityName, feelsLike, minTemperature, maxTemperature, pressure, windSpeed, windDegree, humidity);
            DataContext = CommandForBtn.SharedCommand;

            cityNameTextBlock.Text = cityName;
        }


    }
}
