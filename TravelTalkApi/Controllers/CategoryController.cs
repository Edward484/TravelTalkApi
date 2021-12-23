using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;
using TravelTalkApi.Entities.DTO;
using TravelTalkApi.Repositories;

namespace TravelTalkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController
    {
        private readonly IRepositoryWrapper _repository;

        public CategoryController(AppDbContext ctx, IRepositoryWrapper repository)
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
            var categoriesQuery = _repository.Category.GetAll();
            var categories =
                await (expanded ? categoriesQuery.Include("Topics").ToListAsync() : categoriesQuery.ToListAsync());

            //TODO: Make a CategoryExpandedDTO that extends CategoryDTO and adds the Topics property so we don't send an empty list back on non-expanded result
            var res = categories.Select(el => new CategoryDTO(el)).ToList();

            return res;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _repository.Category.GetByIdAsync(id);

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
            _repository.Category.Create(category);
            await _repository.SaveAsync();

            return new CategoryDTO(category);
        }
    }
}