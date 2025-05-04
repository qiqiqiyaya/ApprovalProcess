using Sm.Core.Actions;

namespace Sm.Register
{
    public record ExecutableActionRecord(
        Dictionary<string, ExecutableActionMap> EntryActionConfigs,
        Dictionary<string, ExecutableActionMap> ExitActionConfigs);
}
