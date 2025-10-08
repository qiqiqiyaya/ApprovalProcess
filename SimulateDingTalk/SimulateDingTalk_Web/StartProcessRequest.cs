using System.Text.Json;

namespace OAApproval.DTOs
{
    public class StartProcessRequest
    {
        public int TemplateId { get; set; }
        public string Title { get; set; }
        public string ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public JsonElement FormData { get; set; }
    }
}