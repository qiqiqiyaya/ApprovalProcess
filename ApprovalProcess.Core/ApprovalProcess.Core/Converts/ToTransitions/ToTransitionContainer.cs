using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ApprovalProcess.Core.Converts.ToTransitions
{
    public class ToTransitionContainer
    {
        private readonly Dictionary<string, ConvertMap> _converters;
        private readonly IServiceProvider _serviceProvider;

        public ToTransitionContainer(Dictionary<string, ConvertMap> converters,
            IServiceProvider serviceProvider)
        {
            _converters = converters;
            _serviceProvider = serviceProvider;
        }

        public IConvertToTransition<TParameter, TState, TTrigger> Get<TParameter, TState, TTrigger>()
        {
            var stateType = typeof(TState);
            var triggerType = typeof(TTrigger);
            var parameterType = typeof(TParameter);

            string key = stateType.FullName + triggerType.FullName + parameterType.FullName;
            if (_converters.TryGetValue(key, out ConvertMap configuration))
            {
                configuration.ConverterCache ??= _serviceProvider.GetRequiredService(configuration.Type);
                if (configuration.ConverterCache is IConvertToTransition<TParameter, TState, TTrigger> converter)
                {
                    return converter;
                }
            }

            throw new Exception($"ConvertToSm not found for type {key}.");
        }
    }
}
