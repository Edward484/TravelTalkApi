using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;
using TravelTalkApi.Entities.Constants;
using TravelTalkApi.Entities.DTO;
using TravelTalkApi.Repositories;

namespace TravelTalkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
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
            var categories = await _repository.Category.GetAll(expanded);
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
        [Authorize("Admin")]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody]CreateCategoryDTO body)
        {
            Category category = new()
            {
                Mods = new List<User>(),
                Name = body.Name,
                Topics = new List<Topic>()
            };
            _repository.Category.Create(category);
            await _repository.SaveAsync();

            return new CategoryDTO(category);
        }

        [HttpPatch]
        [Authorize("Admin")]
        public async Task<ActionResult> ChangeCategoryName([FromBody]ChangeCategoryDTO body)
        {
            try
            {
                _repository.Category.UpdateName(body.CategoryId, body.newName);
                await _repository.SaveAsync();

                return new OkResult();
            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }


        }
        
        [HttpDelete]
        [Authorize("Admin")]
        public async Task<ActionResult> DeleteCategory([FromBody]CategoryIdDTO body)
        {
            try
            {
                var category = await _repository.Category.GetByIdAsync(body.CategoryId);
                _repository.Category.Delete(category);
                await _repository.SaveAsync();

                return new OkResult();
            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }
        }
    }
}