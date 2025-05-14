using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Sm.Core.Converts.ToEntity
{
    public class EntityConvertContainer(
        Dictionary<string, ConvertMap> converters,
        IServiceProvider serviceProvider)
    {
        public IConvertToEntity<TState, TTrigger> Get<TState, TTrigger>()
        {
            var stateType = typeof(TState);
            var triggerType = typeof(TTrigger);

            string key = stateType.FullName + triggerType.FullName;
            if (converters.TryGetValue(key, out ConvertMap configuration))
            {
                configuration.ConverterCache ??= serviceProvider.GetRequiredService(configuration.Type);
                if (configuration.ConverterCache is IConvertToEntity<TState, TTrigger> converter)
                {
                    return converter;
                }
            }

            throw new Exception($"ConvertToSm not found for type {key}.");
        }
    }
}
