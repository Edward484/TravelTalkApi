namespace TravelTalkApi.Models.DTO.Admin
{
    public class AdminDTO
    {
        public AdminDTO(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}