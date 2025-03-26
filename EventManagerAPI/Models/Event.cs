using System.ComponentModel.DataAnnotations;

namespace EventManagerAPI.Models
{
	public class Event
	{
		[Key]
		public Guid EventId { get; set; } = Guid.NewGuid();

		[Required]
		[StringLength(255)]
		public string EventName { get; set; }

		public string? Location { get; set; }
		public string? DressCode { get; set; }
		public string? Concept { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		public string? Description { get; set; }
		public int ParticipantCount { get; set; }

		// Ma generate sau khi tao 1 event
		public string EventCode { get; set; }


		// Foreign Key - Quan hệ 1-N với EventCategory
		public Guid CategoryId { get; set; }
		public EventCategory EventCategory { get; set; }

		// Quan hệ với AppUser (người tổ chức)
		public string OrganizerId { get; set; }
		public AppUser Organizer { get; set; }

		// Quan hệ 1-N với Agenda
		public ICollection<Agenda> Agendas { get; set; }

		// Danh sách người tham gia sự kiện (N-N)
		public ICollection<EventParticipant> EventParticipants { get; set; }
	}
}
