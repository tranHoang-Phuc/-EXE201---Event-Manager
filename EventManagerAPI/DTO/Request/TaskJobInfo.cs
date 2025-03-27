namespace EventManagerAPI.DTO.Request
{
    public class TaskJobInfo
    {
        public Guid? Id { get; set; } // Id có thể null nếu là TaskJob mới
        public string Task { get; set; }
        public string Assignee { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public string RelatedDocuments { get; set; }
        public string Notes { get; set; }
        public Guid EventId { get; set; }
    }
}