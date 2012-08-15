using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsAtAGlance.Repository;
using NewsAtAGlance.Model;
using Microsoft.Practices.Unity;

namespace NewsAtAGlance.Controllers
{
    public class DataServiceController : Controller
    {
        IWeatherConditionRepository _WeatherConditionRepository;

        // When constructor is called, DependencyUnityResolver is called to resolve IWeatherConditionRepository
        public DataServiceController(IWeatherConditionRepository weatherRepo)
        {
            _WeatherConditionRepository = weatherRepo;  
        }

        public ActionResult GetWeatherCondition(string city)
        {
            return Json(_WeatherConditionRepository.GetWeatherCondition(city), JsonRequestBehavior.AllowGet);
        }
    }
}
