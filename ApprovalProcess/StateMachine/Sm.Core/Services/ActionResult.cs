using System.Collections.Generic;
using Sm.Share.Entities;

namespace Sm.Core.Services
{
    public class ActionResult(
        Dictionary<string, ExecutableActionEntity> nameContainer,
        Dictionary<string, ExecutableActionEntity> idContainer)
    {
        public Dictionary<string, ExecutableActionEntity> NameContainer { get; set; } = nameContainer;

        public Dictionary<string, ExecutableActionEntity> IdContainer { get; set; } = idContainer;
    }
}
