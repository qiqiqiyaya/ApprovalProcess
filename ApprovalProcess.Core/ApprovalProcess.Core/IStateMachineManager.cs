using ApprovalProcess.Core.Entities;
using System.Threading.Tasks;

namespace ApprovalProcess.Core
{
    public interface IStateMachineManager
    {
        ValueTask<StateMachineEntity> GetSm(string id);
        ValueTask<StateRepresentationEntity> GetSr(string id);
    }
}
