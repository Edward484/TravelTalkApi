using TravelTalkApi.Entities;

namespace TravelTalkApi.Auth.Policies.PostAuthorPolicy
{
    public interface IPostAuthorPolicy: IAccessPolicy<int,Post>
    {
    }
}