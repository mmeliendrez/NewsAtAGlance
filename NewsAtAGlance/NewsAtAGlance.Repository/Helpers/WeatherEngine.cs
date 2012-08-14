using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NewsAtAGlance.Model;

namespace NewsAtAGlance.Repository.Helpers
{
    /// <summary>
    /// Used to get some fresh weather data into the DB
    /// </summary>
    public class WeatherEngine
    {
        private const string BASE_URL = "http://www.google.com/ig/api?";
        private readonly string _Separator = "&weather=";
        

        public List<WeatherCondition> GetWeatherConditions(string[] cities)
        {
            XDocument doc = CreateXDocument(cities);
            return ParseWeatherConditions(doc);
        }

        private XDocument CreateXDocument(string[] cities)
        {
            string symbolList = String.Join(_Separator, cities);
            string url = string.Concat(BASE_URL, _Separator, symbolList, "&Ticks=", DateTime.Now.Ticks);


            try
            {
                XDocument doc = XDocument.Load(url);
                return doc;
            }
            catch { }
            return null;
        }

        private List<WeatherCondition> ParseWeatherConditions(XDocument doc)
        {
            if (doc == null) return null;
            List<WeatherCondition> weatherConditions = new List<WeatherCondition>();

            IEnumerable<XElement> weatherTags = doc.Root.Descendants("weather");

            foreach (var weatherTag in weatherTags)
            {
                var forecast_information = GetTagData(weatherTag, "forecast_information");
                var current_conditions = GetTagData(weatherTag, "current_conditions");

                var cityCompleteName = GetAttributeData(forecast_information, "city").Replace(" Province","");
                var city = GetAttributeData(forecast_information, "postal_code");
                var forecast_date = GetAttributeData(forecast_information, "forecast_date");

                var condition = GetAttributeData(current_conditions, "condition");
                var tempCelcius = GetAttributeData(current_conditions, "temp_c");
                var humidity = GetAttributeData(current_conditions, "humidity");
                var icon = GetAttributeData(current_conditions, "icon");

                var weatherCondition = new WeatherCondition()
                {
                    City = city,
                    CityCompleteLocation = cityCompleteName,
                    ForecastDate = forecast_date,

                    Condition = condition,
                    TempCelcius = tempCelcius,
                    Humidity = humidity,
                    Icon = icon
                };

                weatherConditions.Add(weatherCondition);
            }
            return weatherConditions;
        }

        private string GetAttributeData(XElement xElement, string elemName)
        {
            return GetTagData(xElement, elemName).Attribute("data").Value;
        }

        private XElement GetTagData(XElement xElement, string elemName)
        {
            return xElement.Elements(elemName).FirstOrDefault();
        }
    }
}
