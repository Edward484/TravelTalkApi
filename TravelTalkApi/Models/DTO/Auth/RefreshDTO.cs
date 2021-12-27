namespace TravelTalkApi.Models.DTO.Auth
{
    public class RefreshDTO
    {
        public string ExpiredAccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}