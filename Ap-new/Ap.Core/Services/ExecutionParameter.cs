namespace Ap.Core.Services
{
    public class ExecutionParameter
    {
        public string FlowId { get; set; }

        public string StateSetId { get; set; }

        public string? ChildStateSetId { get; set; }


        public object User { get; set; }

        public string Trigger { get; set; }
    }
}
