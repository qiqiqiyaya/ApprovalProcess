#nullable enable
using System;

namespace ApprovalProcess.Core.Converts
{

    public class ConvertConfiguration
    {
        public string Key { get; set; }

        public Type Type { get; set; }

        public object? Converter { get; set; }
    }
}
