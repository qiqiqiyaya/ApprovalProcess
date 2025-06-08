namespace Ap.Core.Host
{
    public class ApParameter : ApParameter<object>
    {

    }

    public class ApParameter<T>
    {
        public string Id { get; set; }

        public string? ChildId { get; set; }

        public string Trigger { get; set; }

        public T User { get; set; }
    }
}
