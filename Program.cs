// Этот кринж (модуль API) интегрировать надо
// Дока тут https://www.weatherapi.com/docs/ (на всякий)
// Хотя переменные тут называются максимально понятно

using Newtonsoft.Json;

// Ну ту понятно
public class Program
{
    private static string ApiKey = "885c1735362d4619b1e162629231606";

    public static async Task Main()
    {
        Console.WriteLine("Введите название города:");
        string cityName = Console.ReadLine();
        Console.WriteLine();

        try
        {
            WeatherData weatherData = await GetWeather(cityName);
            LocationData location = weatherData.Location;
            double latitude = location.Latitude;
            double longitude = location.Longitude;
            CurrentData current = weatherData.Current;
            ForecastData forecast = weatherData.Forecast;

            Console.WriteLine($"Город {location.Name}:");
            Console.WriteLine($"Координаты: Широта: {latitude}, Долгота: {longitude}");
            Console.WriteLine($"Текущая температура: {current.TemperatureC}°C ({current.TemperatureF}°F)");
            Console.WriteLine($"Ощущается как: {current.FeelsLikeC}°C ({current.FeelsLikeF}°F)");   // => это просто перебор значений
            Console.WriteLine($"Максимальная температура: {forecast.ForecastDay[0].HourlyWeather.Max(h => h.TemperatureC)}°C ({forecast.ForecastDay[0].HourlyWeather.Max(h => h.TemperatureF)}°F)");
            Console.WriteLine($"Минимальная температура: {forecast.ForecastDay[0].HourlyWeather.Min(h => h.TemperatureC)}°C ({forecast.ForecastDay[0].HourlyWeather.Min(h => h.TemperatureF)}°F)");
            Console.WriteLine($"Давление: {ConvertPressure(current.PressureMb)} мм рт. ст.");
            Console.WriteLine($"Влажность: {current.Humidity}%");
            Console.WriteLine($"Скорость ветра: {ConvertWindSpeed(current.WindSpeed)} м/с");
            Console.WriteLine($"Направление ветра: {current.WindDirection}°");
            Console.WriteLine($"Описание погоды: {current.Condition.Text}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Почасовая информация о погоде на сегодня:");
            Console.WriteLine();

            foreach (HourlyData hourly in forecast.ForecastDay[0].HourlyWeather)
            {
                Console.WriteLine($"Время: {hourly.Time}");
                Console.WriteLine($"Температура: {hourly.TemperatureC}°C ({hourly.TemperatureF}°F)");
                Console.WriteLine($"Ощущается как: {hourly.FeelsLikeC}°C ({hourly.FeelsLikeF}°F)");
                Console.WriteLine($"Давление: {ConvertPressure(hourly.PressureMb)} мм рт. ст.");
                Console.WriteLine($"Влажность: {hourly.Humidity}%");
                Console.WriteLine($"Описание погоды: {hourly.Condition.Text}");
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ну опяяять: {ex.Message}");
        }
    }

    // Тут формируется WeatherData
    public static async Task<WeatherData> GetWeather(string city)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"http://api.weatherapi.com/v1/forecast.json?key={ApiKey}&q={Uri.EscapeDataString(city)}&days=1&aqi=no&alerts=no=no&lang=ru";

            HttpResponseMessage response = await client.GetAsync(url);
            string jsonResponse = await response.Content.ReadAsStringAsync(); 
            WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(jsonResponse);
            
            return weatherData;
            
        }
    }

    // Тут данные о погоде
    public class WeatherData
    {
        [JsonProperty("location")] // Местоположение
        public LocationData Location { get; set; }

        [JsonProperty("current")] // Погода щас
        public CurrentData Current { get; set; }

        [JsonProperty("forecast")] // Погода потом
        public ForecastData Forecast { get; set; }
    }

    // Место
    public class LocationData
    {
        [JsonProperty("name")] // Город
        public string Name { get; set; }

        [JsonProperty("lat")] //Широта
        public double Latitude { get; set; }

        [JsonProperty("lon")] //Долгота
        public double Longitude { get; set; }


    }

    // Погода щас
    public class CurrentData
    {
        [JsonProperty("temp_c")] // Температура в °C
        public double TemperatureC { get; set; }

        [JsonProperty("feelslike_c")] // Ощущаемая температура в °C
        public double FeelsLikeC { get; set; }

        [JsonProperty("temp_f")] // Температура в °F
        public double TemperatureF { get; set; }

        [JsonProperty("feelslike_f")] // Ощущаемая температура в °F
        public double FeelsLikeF { get; set; }

        [JsonProperty("wind_kph")] // Скорость ветра в км/ч
        public double WindSpeed { get; set; }

        [JsonProperty("wind_degree")] // Направление ветра (тут в ответе буквы, поэтому там тоже костыль)
        public string WindDirection { get; set; }

        [JsonProperty("pressure_mb")] // Давление в миллибарах (и тут)
        public double PressureMb { get; set; }

        [JsonProperty("humidity")] // Влажность в % (а тут нет)
        public int Humidity { get; set; }

        [JsonProperty("condition")] // Состояние погоды (оййй)
        public ConditionData Condition { get; set; }
    }

    // Описание (которое мне пришлось костылить)
    public class ConditionData
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    // Погода потом
    public class ForecastData
    {
        [JsonProperty("forecastday")]
        public List<ForecastDataForDay> ForecastDay { get; set; }
    }

    // Погода на весь день
    public class ForecastDataForDay
    {
        [JsonProperty("date")] // Дата
        public string Date { get; set; }

        [JsonProperty("hour")] // Инфа по часам
        public List<HourlyData> HourlyWeather { get; set; }
    }

    // Погода по часам
    public class HourlyData
    {
        [JsonProperty("time")] // Время
        public string Time { get; set; }

        [JsonProperty("temp_c")] // Температура в °C
        public double TemperatureC { get; set; }

        [JsonProperty("feelslike_c")] // Ощущаемая температура в °C
        public double FeelsLikeC { get; set; }

        [JsonProperty("temp_f")] // Температура в градусах °F
        public double TemperatureF { get; set; }

        [JsonProperty("feelslike_f")] // Ощущаемая температура в °F
        public double FeelsLikeF { get; set; }

        [JsonProperty("pressure_mb")] // Давление в миллибарах
        public double PressureMb { get; set; }

        [JsonProperty("humidity")] // Влажность в процентах
        public int Humidity { get; set; }

        [JsonProperty("condition")] // Состояние погоды
        public ConditionData Condition { get; set; }
    }

    // Далее идут костыли, так как я не нашла в документации как это сделать с помощью API:

    // Данный костыль переводит давление в мм рт. ст.
    public static double ConvertPressure(double pressureMb)
    {
        return Math.Round(pressureMb * 0.75);
    }

    // Данный костыль переводит скорость ветра в м/с
    public static double ConvertWindSpeed(double windSpeed)
    {
        return Math.Round(windSpeed / 3.6);
    }
}
