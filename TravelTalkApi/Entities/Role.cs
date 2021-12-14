using System;
using System.ComponentModel.DataAnnotations;

namespace TravelTalkApi.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        [Required]
        public string Title { get; set; }  
    }
}