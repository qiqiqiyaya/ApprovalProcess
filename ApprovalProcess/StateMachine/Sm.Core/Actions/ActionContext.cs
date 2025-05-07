using Microsoft.Extensions.DependencyInjection;
using Sm.Share.Services;
using System;
using System.Collections.Generic;

namespace Sm.Core.Actions
{
    public class ActionContext
    {
        protected readonly IServiceProvider ServiceProvider;
        protected readonly LazyServiceProvider LazyServiceProvider;

        public ActionContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            LazyServiceProvider = new LazyServiceProvider(serviceProvider);
            Properties = new Dictionary<string, object>();
            ObjectPool = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Properties { get; set; }

        /// <summary>
        /// Object Pool
        /// </summary>
        public Dictionary<string, object> ObjectPool { get; set; }

        /// <summary>
        /// get object
        /// </summary>
        /// <returns></returns>
        public T GetObject<T>(string key)
        {
            if (ObjectPool.TryGetValue(key, out var value)) return (T)value;
            return default(T);
        }

        public void AddObject<T>(string key, T value)
        {
            ObjectPool.Add(key, value);
        }

        public T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        public T LazyGetRequiredService<T>()
        {
            return LazyServiceProvider.LazyGetRequiredService<T>();
        }
    }
}
