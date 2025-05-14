using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Sm.Share.Services
{
    public class LazyServiceProvider
    {
        protected IDictionary<Type, object> CachedServices { get; set; }

        protected IServiceProvider ServiceProvider { get; set; }

        public LazyServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            CachedServices = new Dictionary<Type, object>();
        }

        public virtual T LazyGetRequiredService<T>()
        {
            return (T)LazyGetRequiredService(typeof(T));
        }

        public virtual object LazyGetRequiredService(Type serviceType)
        {
            if (CachedServices.TryGetValue(serviceType, out var obj))
            {
                return obj;
            }

            obj = ServiceProvider.GetRequiredService(serviceType);
            CachedServices.Add(serviceType, obj);
            return obj;
        }
    }
}
