namespace TravelTalkApi.Entities.DTO
{
    public class CategoryIdDTO
    {
        public CategoryIdDTO(int categoryId)
        {
            CategoryId = categoryId;
        }

        public int CategoryId { get; set; }
    }
}