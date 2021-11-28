using System;

namespace TravelTalkApi.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Title { get; set; }
        public bool IsMod { get; set; }
    }
}