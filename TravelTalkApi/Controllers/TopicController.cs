using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelTalkApi.Data;
using TravelTalkApi.Entities.DTO.Topic;
using TravelTalkApi.Repositories.TopicRepository;

namespace TravelTalkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController
    {
        private readonly ITopicRepository _repository;

        public TopicController(AppDbContext ctx, ITopicRepository repository)
        {
            _repository = repository;
        }
        

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TopicDTO>> GetTopicById(int id)
        {
            //TODO: Check if a fail SELECT results in NULL or error
            var Topic = await _repository.GetByIdAsync(id);

            return new TopicDTO(Topic);
        }

        //TODO: Implement
        [HttpPost]
        public async Task<ActionResult<TopicDTO>> CreateTopic(CreateTopicDTO body)
        {
           // Create a service that extracts the user from the token and creates a topic based on the body with that user as the author
           throw new NotImplementedException();
        }
    }
}