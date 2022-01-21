using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelTalkApi.Entities;
using TravelTalkApi.Models.DTO.Notification;
using TravelTalkApi.Repositories;
using TravelTalkApi.Services.NotificationService;
using TravelTalkApi.Services.UserService;

namespace TravelTalkApi.Controllers
{
    [Route(("api/[controller]"))]
    [ApiController]
    public class NotificationController
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public NotificationController( 
            IUserService userService, 
            INotificationService notificationService,
            IRepositoryWrapper repositoryWrapper)
        {
            _userService = userService;
            _notificationService = notificationService;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet("current")]
        [Authorize("User")]
        public async Task<ActionResult<List<NotificationDTO>>> GetUserNotification()
        {
            try
            {
                var currentUser = await _userService.GetCurrentUser();
                var listNotifications = await _notificationService.GetAllUserNotification(currentUser);
                List<NotificationDTO> listNotificationsDTO = new();
                
                foreach(Notification notification in listNotifications)
                {
                    var obj = new NotificationDTO(notification);
                    listNotificationsDTO.Add(obj);
                }
                return new OkObjectResult(listNotificationsDTO);
            }
            catch(ArgumentNullException e)
            {
                return new NotFoundObjectResult(new object());
            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        [Authorize("Admin, Mod")]
        public async Task<ActionResult> SendWarningNotificationToAuthor(int postId)
        {
            try
            {
                _notificationService.SendWarningNotificationToAuthor(postId);
                return new OkResult();
            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }
        }


        [HttpPatch]
        [Authorize("Admin")]
        public async Task<ActionResult> ChangeNotificationType(int notificationId)
        {
            try
            {
                _notificationService.ChangeNotificationType(notificationId);
                return new OkResult();
            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }
        }

        [HttpDelete]
        [Authorize("Admin")]
        public async Task<ActionResult> DeleteNotification(int notificationId)
        {
            try
            {
                _notificationService.DeleteNotification(notificationId);
                await _repositoryWrapper.SaveAsync();
                return new OkResult();
            }
            catch (Exception e)
            {
                return new NotFoundResult();
            }
        }
        
        

        
    }
}