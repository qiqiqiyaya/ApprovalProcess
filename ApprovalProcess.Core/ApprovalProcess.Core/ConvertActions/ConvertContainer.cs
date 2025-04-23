using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ApprovalProcess.Core.ConvertActions
{
    public class ConvertContainer
    {
        private readonly Dictionary<string, ConvertConfiguration> _converters;
        private readonly IServiceProvider _serviceProvider;

        public ConvertContainer(Dictionary<string, ConvertConfiguration> converters,
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
            if (_converters.TryGetValue(key, out ConvertConfiguration configuration))
            {
                configuration.Converter ??= _serviceProvider.GetRequiredService(configuration.Type);
                if (configuration.Converter is IConvertToTransition<TParameter, TState, TTrigger> converter)
                {
                    return converter;
                }
            }

            throw new Exception($"ConvertToSm not found for type {key}.");
        }
    }
}
