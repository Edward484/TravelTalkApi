using System;
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

        public CategoryController(AppDbContext ctx, ICategoryRepository repository)
        {
            _repository = repository;
        }

        /**
         * Get all categories
         * If the expanded query param is present, then the Categories will be joined with their topics
         */
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategories(
            [FromQuery(Name = "expanded")] bool expanded)
        {
            var categoriesQuery = _repository.GetAll();
            var categories =
                await (expanded ? categoriesQuery.Include("Topics").ToListAsync() : categoriesQuery.ToListAsync());

            var res = categories.Select(el => new CategoryDTO(el)).ToList();

            return res;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _repository.GetByIdAsync(id);

                return new OkObjectResult(new CategoryDTO(category));
            }
            catch (InvalidOperationException e)
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CreateCategoryDTO body)
        {
            Category category = new Category
            {
                Mods = new List<User>(),
                Name = body.Name,
                Topics = new List<Topic>()
            };
            _repository.Create(category);
            await _repository.SaveAsync();

            return new CategoryDTO(category);
        }
    }
}