using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsAtAGlance.Model;
using NewsAtAGlance.Repository;

namespace NewsAtAGlance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateDatabase()
        {
            NewsAtAGlance.Repository.NewsAtAGlance myDbContext = new NewsAtAGlance.Repository.NewsAtAGlance();

            WeatherConditionRepository respository = new WeatherConditionRepository();

            //var weatherCondition = new WeatherCondition() 
            //{ 
            //    City = "Tandil",
            //    CityCompleteLocation = "Tandil, Buenos Aires",
            //    Condition = "",
            //    ForecastDate = "",
            //    Humidity = "",
            //    Icon = "",
            //    TempCelcius = ""
            //};

            var weatherCondition = respository.GetWeatherCondition("Tandil");

            var operationStatus = respository.InsertWeatherConditionData();

            //myDbContext.WeatherConditions.Add(weatherCondition);

            //int i = myDbContext.SaveChanges();
           
            
            return View("Index");
        }
    }
}
