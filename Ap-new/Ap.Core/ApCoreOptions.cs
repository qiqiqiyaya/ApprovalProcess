using Ap.Core.Builders;
using System;
using System.Collections.Generic;

namespace Ap.Core
{
    public class ApCoreOptions
    {
        internal List<Type> PreBuilders = new();

        public void AddFlow<TPreBuilder>()
            where TPreBuilder : IPreBuilder
        {
            PreBuilders.Add(typeof(TPreBuilder));
        }
    }
}
