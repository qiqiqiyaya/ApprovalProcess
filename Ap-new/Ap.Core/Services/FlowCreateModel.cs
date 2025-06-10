using Ap.Core.Definitions;
using Ap.Core.Services.Interfaces;

namespace Ap.Core.Services
{
    public class FlowCreateModel
    {
        public IUser User { get; set; }

        public string StateSetId { get; set; }
    }
}
