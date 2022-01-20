using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelTalkApi.Auth.Policies.PostAuthorPolicy;
using TravelTalkApi.Entities;
using TravelTalkApi.Entities.DTO;
using TravelTalkApi.Models.DTO.Post;
using TravelTalkApi.Repositories;
using TravelTalkApi.Services.UserService;

namespace TravelTalkApi.Controllers
{
    [Route(("api/[controller]"))]
    [ApiController]
    

    public class PostController
    {
        
        private readonly IRepositoryWrapper _repository;
        private readonly IUserService _userService;
        private readonly IPostAuthorPolicy _postAuthorPolicy;



        public PostController( IRepositoryWrapper repository, IUserService userService, IPostAuthorPolicy postAuthorPolicy)
        {
            _repository = repository;
            _userService = userService;
            _postAuthorPolicy = postAuthorPolicy;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDTO>>> GetPostsInTopicByTopicId(int id)
        {
            try
            {
                var posts = await _repository.PostRepository.GetByTopicId(id);
                return new OkObjectResult(posts);
            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }
        }
        
        [HttpPost]
        [Authorize("User")]
        public async Task<ActionResult<PostDTO>> CreatePost(CreatePostDTO body)
        {
            try
            {
                Post post = new()
                {
                    AuthorId = int.Parse(await _userService.GetCurrentUserId()),
                    CreatedAt = DateTime.Now,
                    Content = body.Content,
                    TopicId = body.TopicId,
                    ImageURL = body.ImageURL,
                    UpvoteCount = 0,
                };
                _repository.PostRepository.Create(post);
                await _repository.SaveAsync();

                return new PostDTO(post);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't create the topic");
            }
        }
        
        [HttpDelete("{postId:int}")]
        [Authorize("User")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var (canAccess, post) = await _postAuthorPolicy.CanAccess(postId);
            if (!canAccess)
            {
                return new ForbidResult();
            }

            _repository.PostRepository.Delete(post);
            await _repository.SaveAsync();

            // Status 204
            return new NoContentResult();
        }

    }
}