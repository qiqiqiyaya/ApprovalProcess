using Ap.Core.Definitions;

namespace Ap.Core.Services
{
    public class FlowCreateModel
    {
        public IUser User { get; set; }

        public IStateSet StateSet { get; set; }
    }
}
