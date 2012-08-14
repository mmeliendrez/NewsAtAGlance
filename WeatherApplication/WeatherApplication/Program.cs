using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WeatherApplication
{
    public class Program
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

                var condition = GetAttributeData(current_conditions, "condition");
                var tempCelcius = GetAttributeData(current_conditions, "temp_c");
                var humidity = GetAttributeData(current_conditions, "humidity");
                var icon = GetAttributeData(current_conditions, "icon");
                    


                //var last = GetDecimal(quote, "last");
                //var change = GetDecimal(quote, "change");
                //var percentChange = GetDecimal(quote, "perc_change");
                //var company = GetAttributeData(quote, "company");

                //if (exchange.ToUpper() == "MUTF") //Handle mutual fund
                //{
                //    var mf = new MutualFund();
                //    mf.Symbol = symbol;
                //    mf.Last = last;
                //    mf.Change = change;
                //    mf.PercentChange = percentChange;
                //    mf.RetrievalDateTime = DateTime.Now;
                //    mf.Company = company;
                //    securities.Add(mf);
                //}
                //else //Handle stock
                //{
                //    var stock = new Stock();
                //    stock.Symbol = symbol;
                //    stock.Last = last;
                //    stock.Change = change;
                //    stock.PercentChange = percentChange;
                //    stock.RetrievalDateTime = DateTime.Now;
                //    stock.Company = company;
                //    stock.Exchange = new Exchange { Title = exchange };
                //    stock.DayHigh = GetDecimal(quote, "high");
                //    stock.DayLow = GetDecimal(quote, "low");
                //    stock.Volume = GetDecimal(quote, "volume");
                //    stock.AverageVolume = GetDecimal(quote, "avg_volume");
                //    stock.MarketCap = GetDecimal(quote, "market_cap");
                //    stock.Open = GetDecimal(quote, "open");
                //    securities.Add(stock);
                //}
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

        static void Main(string[] args)
        {
            var program = new Program();

            string[] cities = new string[2] { "Tandil", "Mar+del+Plata"};


            var result = program.GetWeatherConditions(cities);
        }
    }
}
