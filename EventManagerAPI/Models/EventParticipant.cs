using System.ComponentModel.DataAnnotations;

namespace EventManagerAPI.Models
{
	public class EventParticipant
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		// Foreign Key - Liên kết đến Event
		public Guid EventId { get; set; }
		public Event Event { get; set; }

		// Foreign Key - Liên kết đến AppUser
		public string UserId { get; set; }
		public AppUser User { get; set; }

		// Các thông tin bổ sung về việc tham gia
		public bool IsActive { get; set; }
		public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
	}
}
