#nullable enable
using System;

namespace ApprovalProcess.Core.Converts
{

    public class ConvertMap
    {
        public string Key { get; set; }

        public Type Type { get; set; }

        public object? ConverterCache { get; set; }
    }
}
