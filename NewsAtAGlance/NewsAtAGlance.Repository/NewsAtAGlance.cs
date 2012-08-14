using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using NewsAtAGlance.Model;
using System.Configuration;


namespace NewsAtAGlance.Repository
{
    public class NewsAtAGlance: DbContext
    {
        public NewsAtAGlance() 
        {
            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings[1].ToString();
                
            this.Database.Initialize(false);
        }

        public DbSet<WeatherCondition> WeatherConditions { get; set; }

        public int DeleteWeatherConditions()
        {
            //return base.Database.SqlCommand("DeleteAccounts");
            return base.Database.ExecuteSqlCommand("DeleteWeatherConditions");
        }
    }
}
