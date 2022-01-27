using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TravelTalkApi.Auth.Policies.Utilities;
using TravelTalkApi.Entities;
using TravelTalkApi.Entities.Constants;
using TravelTalkApi.Repositories;

namespace TravelTalkApi.Auth.Policies.PostAuthorPolicy
{
    public class PostAuthorPolicy : IPostAuthorPolicy
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
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var post = await _repositoryWrapper.PostRepository.GetByIdAsync(postId);


                //if it is users post he can delete it
                if (post.AuthorId.ToString().Equals(userId))
                {
                    return new AccessPolicyResult<Post>(true, post);
                }

                // Check if the user is admin
                if (principal.HasClaim(claim =>
                    claim.Type == ClaimTypes.Role && claim.Value == RoleType.Admin))

                {
                    return new AccessPolicyResult<Post>(true, post);
                }

                // Check if the user is categ mod in this categ

                //category moderator can delete the post
                var category = await _repositoryWrapper.Category.GetByTopicId(post.TopicId);
                var categoryWithMods = await _repositoryWrapper.Category.GetByIdWithModsAsync(category.CategoryId);
                var modsIds = categoryWithMods.Mods.Select(user => user.Id);
                if ((principal.HasClaim(claim =>
                         claim.Type == ClaimTypes.Role && claim.Value == RoleType.CategoryMod) &&
                     modsIds.Any(id => id.ToString() == userId)))
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
    }
}