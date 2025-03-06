
namespace EventManagerAPI.DTO.Request
{
    public class EventInfo
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; }

        public string? Location { get; set; }
        public string? DressCode { get; set; }
        public string? Concept { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ParticipantCount { get; set; }
        public Guid CategoryId { get; set; }
        public string OrganizerId { get; set; }


    }
}
