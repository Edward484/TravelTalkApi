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
using TravelTalkApi.Services.NotificationService;
using TravelTalkApi.Services.PostService;
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
        private readonly INotificationService _notificationService;
        private readonly IPostService _postService;



        public PostController( 
            IRepositoryWrapper repository, 
            IUserService userService, 
            IPostAuthorPolicy postAuthorPolicy, 
            INotificationService notificationService,
            IPostService postService)
        {
            _repository = repository;
            _userService = userService;
            _postAuthorPolicy = postAuthorPolicy;
            _notificationService = notificationService;
            _postService = postService;
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
                return await _postService.CreatePostService(body);
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't create the post");
            }
        }

        [HttpPatch("change")]
        [Authorize("User")]
        public async Task<IActionResult> ChangePost(int postId, string content)
        {
            try
            {
                var (canAccess, post) = await _postAuthorPolicy.CanAccess(postId);
                if (!canAccess)
                {
                    return new ForbidResult();
                }

                _repository.PostRepository.UpdateContent(postId, content);
                await _repository.SaveAsync();
                return new OkResult();
            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }
        }
        [HttpPatch]
        [Authorize("User")]
        public async Task<IActionResult> UpVotePost(int postId)
        {
            try
            {
                var post = await _repository.PostRepository.GetByIdAsync(postId);
                post.UpvoteCount++;
                await _repository.SaveAsync();
                _notificationService.SendUpvoteNotification(postId);
                return new OkResult();
            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }
        }
        
        [HttpDelete("current")]
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
            return new OkResult();
        }

    }
}