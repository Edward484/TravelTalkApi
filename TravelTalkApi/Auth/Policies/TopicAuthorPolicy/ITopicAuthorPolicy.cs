using TravelTalkApi.Entities;

namespace TravelTalkApi.Auth.Policies.TopicAuthorPolicy
{
    // An AccessPolicy to update/delete a topic
    // The arg is the id of the topic
    public interface ITopicAuthorPolicy: IAccessPolicy<int,Topic>
    {
    }
}