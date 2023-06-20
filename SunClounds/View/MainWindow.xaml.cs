using Newtonsoft.Json;
using SunClounds.View;
using SunClounds.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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



namespace SunClounds
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ThemeManager.SetThemeBasedOnTime();
            DataContext = CommandForBtn.SharedCommand;
        }



        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string apiKey = "885c1735362d4619b1e162629231606";
            string cityName = cityNameTextBox.Text;
            // http://api.weatherapi.com/v1/forecast.json?key=885c1735362d4619b1e162629231606&q={cityName}&days=1";
            string requestUrl = $"http://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={cityName}&days=7";

            HttpClient httpClient = new HttpClient();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic weatherData = JsonConvert.DeserializeObject(responseBody);

                string temperature = weatherData.current.temp_c;
                string feelsLike = weatherData.current.feelslike_c;
                string minTemperature = weatherData.forecast.forecastday[0].day.mintemp_c.ToString();
                string maxTemperature = weatherData.forecast.forecastday[0].day.maxtemp_c.ToString();
                string pressure = weatherData.current.pressure_mb;
                string windSpeed = weatherData.current.wind_kph;
                string windDegree = weatherData.current.wind_degree;
                string humidity = weatherData.current.humidity;

                Window1 window1 = new Window1(cityName, feelsLike, minTemperature, maxTemperature, pressure, windSpeed, windDegree, humidity);
                window1.Show();
                this.Close();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
