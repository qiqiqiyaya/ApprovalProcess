using Ap.Core.Actions;

namespace Ap.Register
{
    public record ExecutableActionRecord(
        Dictionary<string, ExecutableActionMap> EntryActionConfigs,
        Dictionary<string, ExecutableActionMap> ExitActionConfigs);
}
