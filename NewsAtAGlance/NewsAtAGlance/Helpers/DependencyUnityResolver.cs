using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace NewsAtAGlance.Helpers
{
    // Perform the mapping between ModelContainer and DependencyResolver.SetResolver in Application_Start (Global.asax.cs) 
    public class DependencyUnityResolver : IDependencyResolver
    {
        IUnityContainer _Container;

        public DependencyUnityResolver(IUnityContainer container) 
        {
            _Container = container;
        }

        public object GetService(Type serviceType)
        {
            if (!_Container.IsRegistered(serviceType) && (serviceType.IsAbstract || serviceType.IsInterface))
                return null;

            return _Container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _Container.ResolveAll(serviceType);
        }
    }
}