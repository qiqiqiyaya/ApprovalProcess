using Ap.Core.Share.Entities;
using System.Collections.Generic;

namespace Ap.Core.Services
{
    public class ActionResult(
        Dictionary<string, ExecutableActionEntity> nameContainer,
        Dictionary<string, ExecutableActionEntity> idContainer)
    {
        public Dictionary<string, ExecutableActionEntity> NameContainer { get; set; } = nameContainer;

        public Dictionary<string, ExecutableActionEntity> IdContainer { get; set; } = idContainer;
    }
}
