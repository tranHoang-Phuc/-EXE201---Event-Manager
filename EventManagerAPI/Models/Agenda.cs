using System.ComponentModel.DataAnnotations;

namespace EventManagerAPI.Models
{
	public class Agenda
	{
		[Key]
		public Guid AgendaId { get; set; } = Guid.NewGuid();

		[Required]
		public TimeSpan TimeStart { get; set; }

		[Required]
		public TimeSpan TimeEnd { get; set; }

		public string Description { get; set; }

		// Foreign Key - Quan hệ với Event
		public Guid EventId { get; set; }
		public Event Event { get; set; }
	}
}
