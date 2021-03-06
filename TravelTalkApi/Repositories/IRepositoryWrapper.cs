using System.Threading.Tasks;

namespace TravelTalkApi.Repositories
{
    public interface IRepositoryWrapper
    {
        ICategoryRepository Category { get; }
        INotificationRepository Notification { get; }
        IPostRepository PostRepository { get; }
        ITopicRepository Topic { get; }
        IUserRepository User { get; }


        Task SaveAsync();
    }
}