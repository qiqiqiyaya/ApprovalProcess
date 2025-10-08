namespace OAApproval.DTOs
{
    public class ProcessTaskRequest
    {
        public int TaskId { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; } // "approve" or "reject"
        public string Comment { get; set; }
    }
}