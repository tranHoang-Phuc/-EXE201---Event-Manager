using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagerAPI.Models
{
    public class TaskJob
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Task { get; set; }

        public string? Assignee { get; set; }

        public string? Priority { get; set; }

        public string? Description { get; set; }

        public string? RelatedDocuments { get; set; }

        public string Notes { get; set; }

        [Required]
        public Guid EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}