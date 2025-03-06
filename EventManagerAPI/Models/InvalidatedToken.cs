﻿namespace EventManagerAPI.Models
{
    public class InvalidatedToken
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
