using Miro.Client.Interfaces;
using Miro.Server.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miro.Client.Services
{
    public class UserManager : IUserManager
    {
        private readonly HttpCallManager _httpCallManager;

        public UserManager(HttpCallManager httpCallManager)
        {
            _httpCallManager = httpCallManager;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _httpCallManager.PostAsync<IEnumerable<User>>("users", null);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _httpCallManager.PostAsync<User>("userById", id);
        }

        public async Task<User> GetUserByTokenId(string token)
        {
            return await _httpCallManager.PostAsync<User>("userByToken", token);
        }

        public Task<User> UpdateUser(string token)
        {
            throw new NotImplementedException();
        }
    }
}
