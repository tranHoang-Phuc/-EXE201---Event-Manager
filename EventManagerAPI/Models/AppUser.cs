using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagerAPI.Models
{
	public class AppUser : IdentityUser
	{
		[Column(TypeName = "nvarchar")]
		[StringLength(400)]	
		
		public string? HomeAdress { get; set; }

		public ICollection<EventParticipant>? EventParticipants { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; } = new List<string>();
    }
}
