namespace EventManagerAPI.DTO.Request
{
    public class AgendaInfo
    {
        public Guid? AgendaId { get; set; }

        public TimeSpan TimeStart { get; set; }

        public TimeSpan TimeEnd { get; set; }
        public string Description { get; set; }
        public Guid EventId { get; set; }
    }
}
