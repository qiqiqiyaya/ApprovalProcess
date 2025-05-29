using System;

namespace Sm.Core.Converts
{

    public class ConvertMap
    {
        public string Key { get; set; }

        public Type Type { get; set; }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public object? ConverterCache { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}
