namespace TravelTalkApi.Models.DTO.Admin
{
    public class AdminDTO
    {
        public AdminDTO(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}