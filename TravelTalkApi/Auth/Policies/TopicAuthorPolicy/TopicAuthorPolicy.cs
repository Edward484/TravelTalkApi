using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TravelTalkApi.Auth.Policies.Utilities;
using TravelTalkApi.Entities;
using TravelTalkApi.Repositories;

namespace TravelTalkApi.Auth.Policies.TopicAuthorPolicy
{
    public class TopicAuthorPolicy : ITopicAuthorPolicy
    {
        private HttpContext _httpContext;
        private IRepositoryWrapper _repositoryWrapper;

        public TopicAuthorPolicy(IHttpContextAccessor contextAccessor, IRepositoryWrapper repositoryWrapper)
        {
            _httpContext = contextAccessor.HttpContext;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<AccessPolicyResult<Topic>> CanAccess(int topicId)
        {
            var principal = _httpContext.User;
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            var topic = await _repositoryWrapper.Topic.GetByIdAsync(topicId);
            return new AccessPolicyResult<Topic>(topic.AuthorId.ToString().Equals(userId), topic);
        }
    }
}