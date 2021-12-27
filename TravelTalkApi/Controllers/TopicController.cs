using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelTalkApi.Data;
using TravelTalkApi.Entities.DTO;
using TravelTalkApi.Repositories;

namespace TravelTalkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController
    {
        private readonly IRepositoryWrapper _repository;
        private readonly HttpContext? _httpContext;

        public TopicController(AppDbContext ctx, IRepositoryWrapper repository,IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContext = httpContextAccessor.HttpContext;
        }
        

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTopicById(int id)
        {
            try
            {
                var Topic = await _repository.Topic.GetByIdAsync(id);
                return new OkObjectResult(new TopicDTO(Topic));
            }
            catch (InvalidOperationException e)
            {
                return new NotFoundResult();
            }

         
        }

        //TODO: Implement
        [HttpPost]
        [Authorize( "User")]
        public async Task<ActionResult<TopicDTO>> CreateTopic(CreateTopicDTO body)
        {
            var claims = _httpContext?.User.Identity;
           // Create a service that extracts the user from the token and creates a topic based on the body with that user as the author
           throw new NotImplementedException();
        }
    }
}