#nullable enable
namespace Sm.Core.Actions.Models
{
    /// <summary>
    /// 状态设置动作
    /// </summary>
    public class StateSettingAction
    {
        public string Name { get; set; }

        public ActionConfiguration? Configuration { get; set; }

        public StateSettingAction(string name) : this(name, null)
        {
        }

        public StateSettingAction(string name, ActionConfiguration configuration)
        {
            Name = name;
            Configuration = configuration;
        }
    }
}
