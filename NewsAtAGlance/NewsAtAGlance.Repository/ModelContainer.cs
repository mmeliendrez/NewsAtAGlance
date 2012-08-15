using Microsoft.Practices.Unity;
using NewsAtAGlance.Repository;


namespace NewsAtAGlance.Model
{
    //This is a simplified version of the code shown in the videos
    //The instance of UnityContainer is created in the constructor 
    //rather than checking in the Instance property and performing a lock if needed
    public static class ModelContainer
    {
        private static readonly object _Key = new object();
        private static UnityContainer _Instance;

        static ModelContainer()
        {
            _Instance = new UnityContainer();
        }

        public static UnityContainer Instance
        {
            get
            {
                _Instance.RegisterType<IWeatherConditionRepository, WeatherConditionRepository>(new HierarchicalLifetimeManager());
                return _Instance;
            }
        }
    }
}
