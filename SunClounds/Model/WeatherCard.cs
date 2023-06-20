using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunClounds.Model
{
    public class WeatherCardData
    {
        public string WeatherIcon { get; set; }
        public string Time { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string FeelsLike { get; set; }
        public string Condition { get; set; }
    }
}
