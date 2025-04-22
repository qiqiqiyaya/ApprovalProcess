using System;
using System.Collections.Generic;
using System.Text;

namespace ApprovalProcess.Core.ConvertActions
{
    public class ConvertContainer
    {
        private readonly IDictionary<string, Type> _convertToType;
        private readonly IDictionary<string, object> _convertToObj;
        private readonly IServiceProvider _serviceProvider;

        public ConvertContainer(Dictionary<string, Type> convertToType, IServiceProvider serviceProvider)
        {
            _convertToType = convertToType;
            _serviceProvider = serviceProvider;
            _convertToObj = new Dictionary<string, IToTransition>();
        }

        public IToTransition Get<TState, TTrigger>(Type type)
        {

            string key = type.FullName;
            //if (_convertToObj.TryGetValue(key, out IToTransition co))
            //{
            //    return co;
            //}

            if (_convertToType.TryGetValue(key, out Type typeObj))
            {
                IToTransition toTransition = (IToTransition)Activator.CreateInstance(typeObj, _serviceProvider);
                _convertToObj.Add(key, toTransition);
                return toTransition;
            }

            throw new Exception($"ConvertToSm not found for type {type.FullName}.");
        }

    }

    public class ConvertInfo
    {
        public string Key { get; set; }


    }
}
