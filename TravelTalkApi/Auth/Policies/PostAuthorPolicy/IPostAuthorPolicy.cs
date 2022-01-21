using System.Threading.Tasks;
using TravelTalkApi.Auth.Policies.Utilities;
using TravelTalkApi.Entities;

namespace TravelTalkApi.Auth.Policies.PostAuthorPolicy
{
    public interface IPostAuthorPolicy: IAccessPolicy<int,Post>
    {
    }
}