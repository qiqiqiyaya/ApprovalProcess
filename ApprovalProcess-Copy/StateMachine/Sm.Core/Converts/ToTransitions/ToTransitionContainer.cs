using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Sm.Core.Converts.ToTransitions
{
    public class ToTransitionContainer(
        Dictionary<string, ConvertMap> converters,
        IServiceProvider serviceProvider)
    {
        public IConvertToTransition<TParameter, TState, TTrigger> Get<TParameter, TState, TTrigger>()
        {
            var stateType = typeof(TState);
            var triggerType = typeof(TTrigger);
            var parameterType = typeof(TParameter);

            string key = stateType.FullName + triggerType.FullName + parameterType.FullName;
            if (converters.TryGetValue(key, out ConvertMap configuration))
            {
                configuration.ConverterCache ??= serviceProvider.GetRequiredService(configuration.Type);
                if (configuration.ConverterCache is IConvertToTransition<TParameter, TState, TTrigger> converter)
                {
                    return converter;
                }
            }

            throw new Exception($"ConvertToSm not found for type {key}.");
        }
    }
}
