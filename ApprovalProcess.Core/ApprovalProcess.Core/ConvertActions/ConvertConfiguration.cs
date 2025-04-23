using System;

namespace ApprovalProcess.Core.ConvertActions
{

    public class ConvertConfiguration
    {
        public string Key { get; set; }

        public Type Type { get; set; }

        public object? Converter { get; set; }
    }
}
