﻿namespace TravelTalkApi.Models.DTO.Auth
{
    public class LoginResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}