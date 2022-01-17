using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TravelTalkApi.Auth.Policies.Utilities;
using TravelTalkApi.Entities;
using TravelTalkApi.Repositories;

namespace TravelTalkApi.Auth.Policies.PostAuthorPolicy
{
    public class PostAuthorPolicy
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
            var principal = _httpContext.User;
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            var post = await _repositoryWrapper.PostRepository.GetByIdAsync(postId);
            if(post.AuthorId.ToString().Equals(userId))
            {
                return new AccessPolicyResult<Post>(true, post);
            }
            else
            {
                return new AccessPolicyResult<Post>(false, post);
            }
        }
        //TODO Implement acces for moderators
        
    }
}