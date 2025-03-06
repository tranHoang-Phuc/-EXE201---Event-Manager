namespace EventManagerAPI.DTO.Request
{
    public class EPInfo
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public string UserId { get; set; }

        public bool IsActive { get; set; }
    }
}
