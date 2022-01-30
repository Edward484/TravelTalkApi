using System;
using System.Threading.Tasks;
using TravelTalkApi.Entities;
using TravelTalkApi.Models.DTO.Post;
using TravelTalkApi.Repositories;
using TravelTalkApi.Services.NotificationService;
using TravelTalkApi.Services.UserService;

namespace TravelTalkApi.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;



        public PostService( 
            IRepositoryWrapper repository, 
            IUserService userService, 
            INotificationService notificationService)
        {
            _repository = repository;
            _userService = userService;
            _notificationService = notificationService;
        }
        
        
        public async Task<PostDTO> CreatePost(CreatePostDTO body)
        {
            var authorId = int.Parse(await _userService.GetCurrentUserId());
            var author = await _repository.User.GetByIdComplete(authorId);
            Post post = new()
            {
                Author = author,
                AuthorId = authorId,
                CreatedAt = DateTime.Now,
                Content = body.Content,
                TopicId = body.TopicId,
                ImageURL = body.ImageURL,
                UpvoteCount = 0,
            };
            _repository.PostRepository.Create(post);
            await _repository.SaveAsync();
            _notificationService.SendCommentNotification(body.TopicId);

            return new PostDTO(post);
        }
    }
}