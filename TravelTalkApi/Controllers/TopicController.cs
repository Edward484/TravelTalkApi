using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelTalkApi.Auth.Policies.TopicAuthorPolicy;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;
using TravelTalkApi.Entities.DTO;
using TravelTalkApi.Repositories;
using TravelTalkApi.Services.UserService;

namespace TravelTalkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IUserService _userService;
        private readonly ITopicAuthorPolicy _topicAuthorPolicy;

        public TopicController(AppDbContext ctx, IRepositoryWrapper repository, IUserService userService,
            ITopicAuthorPolicy topicAuthorPolicy)
        {
            _repository = repository;
            _userService = userService;
            _topicAuthorPolicy = topicAuthorPolicy;
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTopicById(int id, [FromQuery(Name = "expanded")] bool expanded)
        {
            try
            {
                var topic = await _repository.Topic.GetByIdAsync(id,expanded);
                return new OkObjectResult(new TopicDTO(topic));
            }
            catch (InvalidOperationException e)
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        [Authorize("User")]
        public async Task<IActionResult> CreateTopic(CreateTopicDTO body)
        {
            try
            {
                var currentUser = await _userService.GetCurrentUser();
                var topic = new Topic()
                {
                    Title = body.Title,
                    Description = body.Description,
                    Author = currentUser,
                    CategoryId = body.CategoryId
                };
                _repository.Topic.Create(topic);
                await _repository.SaveAsync();
                return new OkObjectResult(new TopicDTO(topic));
            }
            catch (Exception e)
            {
                // Something went wrong
                // If we reach this, it means no user was found
                // But we know we got a user since we passed auth check
                return new StatusCodeResult(500);
            }
        }

        [HttpPatch("{topicId:int}")]
        [Authorize("User")]
        public async Task<IActionResult> UpdateTopicDescription(UpdateTopicDTO body, int topicId)
        {
            var (canAccess, topic) = await _topicAuthorPolicy.CanAccess(topicId);
            if (!canAccess)
            {
                return new ForbidResult();
            }

            topic.Description = body.Description;

            _repository.Topic.Update(topic);
            await _repository.SaveAsync();

            // Status 204
            return new NoContentResult();
        }

        [HttpDelete("{topicId:int}")]
        [Authorize("User")]
        public async Task<IActionResult> DeleteTopic(int topicId)
        {
            var (canAccess, topic) = await _topicAuthorPolicy.CanAccess(topicId);
            if (!canAccess)
            {
                return new ForbidResult();
            }

            _repository.Topic.Delete(topic);
            await _repository.SaveAsync();

            // Status 204
            return new NoContentResult();
        }
    }
}