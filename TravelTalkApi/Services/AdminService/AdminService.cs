﻿using System.Linq;
using Microsoft.AspNetCore.Identity;
using TravelTalkApi.Entities;
using TravelTalkApi.Entities.Constants;
using TravelTalkApi.Repositories;

namespace TravelTalkApi.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public async void GiveUserRole(int userId, string role)
        {
            var user = await _repositoryWrapper.User.GetByIdWithRoles(userId);
            await _userManager.AddToRoleAsync(user, role);
        }

        public async void RemoveUserRole(int userId, string role)
        {
            var user = await _repositoryWrapper.User.GetByIdWithRoles(userId);
            await _userManager.RemoveFromRoleAsync(user, role);
        }
        

        public async void MakeUserCategoryMod(int userId, int categoryId)
        {
            var user = await _repositoryWrapper.User.GetByIdWithRoles(userId);
            if (user.UserRoles.All(role => role.Role.Name != RoleType.CategoryMod))
            {
                await _userManager.AddToRoleAsync(user, RoleType.CategoryMod);
            }

            var category = await _repositoryWrapper.Category.GetByIdWithModesAsync(categoryId);
            category.Mods.Add(user);
            await _repositoryWrapper.SaveAsync();
        }

        public async void RemoveUserCategoryMod(int userId, int categoryId)
        {
            var user = await _repositoryWrapper.User.GetByIdWithRoles(userId);
            if (user.UserRoles.Any(role => role.Role.Name != RoleType.CategoryMod))
            {
                await _userManager.RemoveFromRoleAsync(user, RoleType.CategoryMod);
            }

            var category = await _repositoryWrapper.Category.GetByIdWithModesAsync(categoryId);
            var userFromMods = category.Mods.First(mod => mod.Id == user.Id);
            category.Mods.Remove(userFromMods);
            await _repositoryWrapper.SaveAsync();
        }
    }
}