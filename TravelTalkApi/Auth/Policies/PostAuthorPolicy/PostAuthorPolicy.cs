using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TravelTalkApi.Auth.Policies.Utilities;
using TravelTalkApi.Entities;
using TravelTalkApi.Repositories;

namespace TravelTalkApi.Auth.Policies.PostAuthorPolicy
{
    public class PostAuthorPolicy: IPostAuthorPolicy
    {
        
        private HttpContext _httpContext;
        private IRepositoryWrapper _repositoryWrapper;

        public PostAuthorPolicy(IHttpContextAccessor contextAccessor, IRepositoryWrapper repositoryWrapper)
        {
            _httpContext = contextAccessor.HttpContext;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<AccessPolicyResult<Post>> CanAccess(int postId)
        {
            try
            {
                var principal = _httpContext.User;
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                var post = await _repositoryWrapper.PostRepository.GetByIdAsync(postId);
                var user = await _repositoryWrapper.User.GetByIdWithRoles(Int32.Parse(userId));
                
                //category moderator can delete the post
                var currentCateg = await _repositoryWrapper.Category.GetByTopicId(post.TopicId);
                var categMod = await _repositoryWrapper.Category.GetByIdWithModsAsync(currentCateg.CategoryId);



                //any admin can delete any post
                foreach (var role in user.UserRoles)
                {
                    if (role.RoleId == 1)
                    {
                        return new AccessPolicyResult<Post>(true, post);
                    }
                }
                //if it is users post he can delete it
                if (post.AuthorId.ToString().Equals(userId))
                {
                    return new AccessPolicyResult<Post>(true, post);
                }
                else
                {
                    return new AccessPolicyResult<Post>(false, post);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error at verifying access policy on post");
            }
        }
        //TODO Implement aces for categoryModerators 
        //verific daca e categoryModerator. 
        //CategoryMod verfic daca categoria selectata in acesta lista
        
    }
}