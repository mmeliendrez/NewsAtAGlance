using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsAtAGlance.Model;
using NewsAtAGlance.Repository;

namespace NewsAtAGlanc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
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

            //var weatherCondition = respository.GetWeatherCondition("Mar+del+Plata");

            var operationStatus = respository.InsertWeatherConditionData();

            //myDbContext.WeatherConditions.Add(weatherCondition);

            //int i = myDbContext.SaveChanges();
           
            
            return View("Index");
        }
    }
}
