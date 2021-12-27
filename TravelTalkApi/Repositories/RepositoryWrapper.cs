using System;
using System.Threading.Tasks;
using TravelTalkApi.Data;

namespace TravelTalkApi.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _context;

        private ICategoryRepository _category;
        private INotificationRepository _notification;
        private IPostRepository _post;
        private ITopicRepository _topic;
        private IUserRepository _user;


        public RepositoryWrapper(AppDbContext context)
        {
            _context = context;
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_context);
                }

                return _category;
            }
        }

        public INotificationRepository Notification
        {
            get
            {
                if (_notification == null)
                {
                    _notification = new NotificationRepository(_context);
                }

                return _notification;
            }
        }

        public IPostRepository PostRepository
        {
            get
            {
                if (_post == null)
                {
                    _post = new PostRepository(_context);
                }

                return _post;
            }
        }

        public ITopicRepository Topic
        {
            get
            {
                if (_topic == null)
                {
                    _topic = new TopicRepository(_context);
                }

                return _topic;
            }
        }
        public IUserRepository User
        {
            get
            {
                if (_user == null) _user = new UserRepository(_context);
                return _user;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}