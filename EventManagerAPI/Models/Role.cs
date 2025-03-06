using Microsoft.AspNetCore.Identity;

namespace EventManagerAPI.Models
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}
