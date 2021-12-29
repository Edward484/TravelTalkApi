﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TravelTalkApi.Entities;
using TravelTalkApi.Repositories;

namespace TravelTalkApi.Services.UserService
{
    public class UserService:IUserService
    {
        private readonly HttpContext? _httpContext;
        private readonly IRepositoryWrapper _repository;

        public UserService(IHttpContextAccessor httpContextAccessor,IRepositoryWrapper repositoryWrapper)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _repository = repositoryWrapper;
        }

        public Task<User> GetCurrentUser()
        {
            var email = _httpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
            {
                throw new Exception("User not found");
            }

            return _repository.User.GetUsersByEmail(email);
        }
    }
}