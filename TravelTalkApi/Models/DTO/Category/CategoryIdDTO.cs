namespace TravelTalkApi.Entities.DTO
{
    public class CategoryIdDTO
    {
        public CategoryIdDTO(Category category)
        {
            CategoryId = category.CategoryId;
        }

        public int CategoryId { get; set; }
    }
}