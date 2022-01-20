using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelTalkApi.Entities;
using TravelTalkApi.Models.DTO.Notification;
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

        public NotificationController( IUserService userService, INotificationService notificationService)
        {
            _userService = userService;
            _notificationService = notificationService;
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

        
    }
}