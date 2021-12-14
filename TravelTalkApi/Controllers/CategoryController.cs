using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;
using TravelTalkApi.Entities.DTO;
using TravelTalkApi.Repositories.CategoryRepository;

namespace TravelTalkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(AppDbContext ctx,ICategoryRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategories()
        {
            var categories = await _repository.GetAll().ToListAsync();

            var res = categories.Select(el => new CategoryDTO(el)).ToList();

            return res;
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            //TODO: Check if a fail SELECT results in NULL or error
            var category = await _repository.GetByIdAsync(id);

            return new CategoryDTO(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCAtegory(CreateCategoryDTO body)
        {
            Category category = new Category();
            category.Mods = new List<User>();
            category.Name = body.Name;
            category.Topics = new List<Topic>();
            _repository.Create(category);
            await _repository.SaveAsync();

            return new CategoryDTO(category);
        }

    }
}