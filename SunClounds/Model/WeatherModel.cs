using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunClounds.Model
{
  
    public class WeatherModel
    {
        [JsonProperty("location")]
        public LocationModel Location { get; set; }

        [JsonProperty("current")]
        public CurrentWeatherModel CurrentWeather { get; set; }

        [JsonProperty("forecast")]
        public ForecastModel Forecast { get; set; }
    }

    public class LocationModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }

    public class CurrentWeatherModel
    {
        [JsonProperty("temp_c")]
        public double TemperatureC { get; set; }

        [JsonProperty("feelslike_c")]
        public double FeelsLikeC { get; set; }

        [JsonProperty("temp_f")]
        public double TemperatureF { get; set; }

        [JsonProperty("feelslike_f")]
        public double FeelsLikeF { get; set; }

        [JsonProperty("wind_kph")]
        public double WindSpeed { get; set; }

        [JsonProperty("wind_degree")]
        public string WindDirection { get; set; }

        [JsonProperty("pressure_mb")]
        public double PressureMb { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("condition")]
        public ConditionModel Condition { get; set; }
    }

    public class ConditionModel
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class ForecastModel
    {
        [JsonProperty("forecastday")]
        public List<ForecastDayModel> ForecastDays { get; set; }
    }

    public class ForecastDayModel
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("hour")]
        public List<HourlyWeatherModel> HourlyWeather { get; set; }
    }

    public class HourlyWeatherModel
    {
        private string time;

        [JsonProperty("time")]
        public string Time
        {
            get { return time; }
            set
            {
                // Parse the time string to DateTime and format it as "HH:mm"
                if (DateTime.TryParse(value, out DateTime parsedTime))
                    time = parsedTime.ToString("HH:mm");
                else
                    time = value;
            }
        }

        [JsonProperty("temp_c")]
        public double TemperatureC { get; set; }

        [JsonProperty("feelslike_c")]
        public double FeelsLikeC { get; set; }

        [JsonProperty("temp_f")]
        public double TemperatureF { get; set; }

        [JsonProperty("feelslike_f")]
        public double FeelsLikeF { get; set; }

        [JsonProperty("pressure_mb")]
        public double PressureMb { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("condition")]
        public ConditionModel Condition { get; set; }

        public string ImageUrl
        {
            get
            {
                string condition = Condition?.Text?.ToLower();
                switch (condition)
                {
                    case "солнечно":
                        return "/img/Sunny.png";
                    case "ясно":
                        return "/img/Sunny.png";
                    case "дождь":
                        return "/img/Rainy.png";
                    case "переменная облачность":
                        return "/img/Cloudy.png";
                    case "облачно":
                        return "/img/Cloudy.png";
                    case "снег":
                        return "/img/Snow.png";
                    case "thunderstorm":
                        return "/img/Thunderstorm.png";
                    case "пасмурно":
                        return "/img/Downpour.png";
                    default:
                        return null;
                }
            }
        }
    }
}
