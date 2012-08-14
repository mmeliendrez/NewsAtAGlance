using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherApplication
{
    public class WeatherCondition
    {
        // Primitive properties
        public int Id { get; set; }
        public string City { get; set; }
        public string CityCompleteLocation { get; set; }
        public string ForecastDate { get; set; }
        public string Condition { get; set; }
        public string TempCelcius { get; set; }
        public string Humidity { get; set; }
        public string Icon { get; set; }
    }
}
