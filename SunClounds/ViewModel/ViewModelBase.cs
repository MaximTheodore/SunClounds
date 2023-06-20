using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using SunClounds.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SunClounds.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherModel weatherData;
        private string cityName;
        private bool isLoading;
        private string errorMessage;
        private static string ApiKey = "885c1735362d4619b1e162629231606";
        public List<HourlyWeatherModel> HourlyWeather { get; set; }



        public WeatherViewModel(string city)
        {
            LoadDataCommand = new RelayCommand(LoadData);
            ChangeChartCommand = new RelayCommand<string>(ChangeChart);
            CityName = city;
            GetWeather(city);
        }
       

        public WeatherModel WeatherData
        {
            get { return weatherData; }
            set { SetProperty(ref weatherData, value); }
        }

        public string CityName
        {
            get { return cityName; }
            set { SetProperty(ref cityName, value); }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public ICommand LoadDataCommand { get; }

        private async void LoadData()
        {
            IsLoading = true;
            ErrorMessage = null;

            try
            {
                WeatherData = await GetWeather(CityName);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка загрузки данных: {ex.Message}";
            }

            IsLoading = false;
        }
        public List<string> HourlyWeatherTimes
        {
            get
            {
                if (WeatherData != null && WeatherData.Forecast != null && WeatherData.Forecast.ForecastDays != null &&
                    WeatherData.Forecast.ForecastDays.Count > 0 && WeatherData.Forecast.ForecastDays[0].HourlyWeather != null)
                {
                    return WeatherData.Forecast.ForecastDays[0].HourlyWeather.Select(w => w.Time).ToList();
                }
                return null;
            }
        }

        private async Task<WeatherModel> GetWeather(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://api.weatherapi.com/v1/forecast.json?key={ApiKey}&q={Uri.EscapeDataString(city)}&days=1&aqi=no&alerts=no=no&lang=ru";

                HttpResponseMessage response = await client.GetAsync(url);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                WeatherModel weatherData = JsonConvert.DeserializeObject<WeatherModel>(jsonResponse);

                WeatherData = weatherData; // Assign value to WeatherData property

                return weatherData;
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        #endregion

        public ICommand ChangeChartCommand { get; }

        private SeriesCollection chartSeries;

        public SeriesCollection ChartSeries
        {
            get { return chartSeries; }
            set { SetProperty(ref chartSeries, value); }
        }

        private string selectedChart;

        private void ChangeChart(string parameter)
        {
            selectedChart = parameter;
            UpdateChart();
        }

        private void UpdateChart()
        {
            if (WeatherData == null || WeatherData.Forecast == null || WeatherData.Forecast.ForecastDays == null)
                return;

            var forecastDay = WeatherData.Forecast.ForecastDays.FirstOrDefault();
            if (forecastDay == null || forecastDay.HourlyWeather == null)
                return;

            var hourlyWeather = forecastDay.HourlyWeather;

            var chartPoints = new ChartValues<ObservablePoint>();

            switch (selectedChart)
            {
                case "Temperature":
                    chartPoints.AddRange(hourlyWeather.Select((hourly, index) => new ObservablePoint(index, (int)hourly.TemperatureC)));
                    break;
                case "FeelsLike":
                    chartPoints.AddRange(hourlyWeather.Select((hourly, index) => new ObservablePoint(index, (int)hourly.FeelsLikeC)));
                    break;
                case "Pressure":
                    chartPoints.AddRange(hourlyWeather.Select((hourly, index) => new ObservablePoint(index, (int)hourly.PressureMb)));
                    break;
            }

            ChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Values = chartPoints,
                    PointGeometrySize = 5,
                    LineSmoothness = 0.2
                }
            };
        }

        private string time;

        public string Time { get => time; set => SetProperty(ref time, value); }

        private string temperature;

        public string Temperature { get => temperature; set => SetProperty(ref temperature, value); }

        private string humidity;

        public string Humidity { get => humidity; set => SetProperty(ref humidity, value); }

        private string feelsLike;

        public string FeelsLike { get => feelsLike; set => SetProperty(ref feelsLike, value); }
    }
   
}
