﻿namespace EventManagerAPI.DTO.Request
{
    public class ResetPasswordRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
