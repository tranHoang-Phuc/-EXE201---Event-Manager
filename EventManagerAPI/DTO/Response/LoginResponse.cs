using System.ComponentModel.DataAnnotations;

namespace EventManagerAPI.DTO.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public bool Authenticated { get; set; }
    }
}
