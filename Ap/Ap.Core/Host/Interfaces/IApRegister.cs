using Ap.Core.Definitions;
using System.Threading.Tasks;

namespace Ap.Core.Host.Interfaces
{
    public interface IApRegister
    {
        ValueTask Register(IStateSet stateSet);
    }
}
