using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using NewsAtAGlance.Repository.Helpers;
using System.Transactions;
using NewsAtAGlance.Model;

namespace NewsAtAGlance.Repository
{
    public class WeatherConditionRepository : RepositoryBase<NewsAtAGlance>, IWeatherConditionRepository
    {
        bool localDataOnly = Boolean.Parse(ConfigurationManager.AppSettings["LocalDataOnly"]);
        
        //Some random symbols to use in order to get data into the database
        private readonly string[] _Cities = {"Tandil", "Mar+del+Plata"};

        public WeatherCondition GetWeatherCondition(string cityName)
        {
            using (DataContext)
            {
                var weatherCondition = DataContext.WeatherConditions.SingleOrDefault(w => w.City == cityName);   // First, WeatherCondition get from database

                if (weatherCondition == null)   // If it is not present in database, get ir from Google Service
                {
                    var engine = new WeatherEngine();

                    string[] cities = { cityName };
                    weatherCondition = engine.GetWeatherConditions(cities).FirstOrDefault();

                    DataContext.WeatherConditions.Add(weatherCondition);

                    var opStatus = Save(weatherCondition);
                    if (!opStatus.Status)
                    {
                        weatherCondition = new WeatherCondition { CityCompleteLocation = "Error getting weather condition." };
                    }
                }

                return weatherCondition;
            }
        }

        public OperationStatus UpdateWeatherConditions()
        {
            var opStatus = new OperationStatus { Status = true };
            if (localDataOnly) return opStatus;

            var weatherConditions = DataContext.WeatherConditions; //Get existing securities
            var engine = new WeatherEngine();
            var updatedWeatherConditions = engine.GetWeatherConditions(weatherConditions.Select(w => w.City).ToArray());
            //Return if updatedSecurities is null
            if (updatedWeatherConditions == null) return new OperationStatus { Status = false };

            foreach (var weatherCondition in weatherConditions)
            {
                //Grab updated version of security
                var updatedWeatherCondition = updatedWeatherConditions.Single(uw => uw.City == weatherCondition.City);
                weatherCondition.Condition = updatedWeatherCondition.Condition;
                weatherCondition.ForecastDate = updatedWeatherCondition.ForecastDate;
                weatherCondition.Humidity = updatedWeatherCondition.Humidity;
                weatherCondition.Icon = updatedWeatherCondition.Icon;
                weatherCondition.TempCelcius = updatedWeatherCondition.TempCelcius;
                
                DataContext.Entry(weatherCondition).State = System.Data.EntityState.Modified;
            }

            //Insert records
            try
            {
                DataContext.SaveChanges();
            }
            catch (Exception exp)
            {
                return OperationStatus.CreateFromException("Error updating security quote.", exp);
            }
            return opStatus;
        }

        public OperationStatus InsertWeatherConditionData()
        {
            var engine = new WeatherEngine();
            var weatherConditions = engine.GetWeatherConditions(_Cities);

            if (weatherConditions != null && weatherConditions.Count > 0)
            {
                using (var ts = new TransactionScope())
                {
                    using (DataContext)
                    {
                        var opStatus = DeleteWeatherConditionRecords(DataContext);
                        if (!opStatus.Status) return opStatus;

                        opStatus = InsertWeatherConditions(weatherConditions, DataContext);
                        if (!opStatus.Status) return opStatus;
                    }
                    ts.Complete();
                }
            }
            return new OperationStatus { Status = true };
        }

        private static OperationStatus InsertWeatherConditions(IEnumerable<WeatherCondition> weatherConditions, NewsAtAGlance context)
        {
            foreach (var weatherCondition in weatherConditions)
            {
                //Add weatherCondition into collection and then insert into DB
                context.WeatherConditions.Add(weatherCondition);
            }

            //Insert records
            try
            {
                context.SaveChanges();
            }
            catch (Exception exp)
            {
                return OperationStatus.CreateFromException("Error updating Weather Conditions.", exp);
            }
            return new OperationStatus { Status = true };
        }

        private OperationStatus DeleteWeatherConditionRecords(NewsAtAGlance context)
        {
            var opStatus = new OperationStatus { Status = false };
            try
            {
                opStatus.Status = context.DeleteWeatherConditions() == 0;
            }
            catch (Exception exp)
            {
                return OperationStatus.CreateFromException("Error deleting Weather Conditions data.", exp);
            }
            return new OperationStatus { Status = true };
        }
    }
}
