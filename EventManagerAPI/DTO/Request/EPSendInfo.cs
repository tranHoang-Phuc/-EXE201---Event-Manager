namespace EventManagerAPI.DTO.Request
{
    public class EPSendInfo
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string UserId { get; set; }
        public string EventName { get; set; } 
        public DateTime StartDate { get; set; }

        public string CategoryName { get; set; }
        
    }
}
