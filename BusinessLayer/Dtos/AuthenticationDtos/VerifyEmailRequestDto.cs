﻿namespace BusinessLayer.DTOs.AuthenticationDtos
{
    public class VerifyEmailRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
