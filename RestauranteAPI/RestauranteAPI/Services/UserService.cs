using System;
using System.Collections.Generic;
using System.Linq;
using RestauranteAPI.Models;
using System.Threading.Tasks;
using RestauranteAPI.Repositories;
using RestauranteAPI.Services.Injections;
using RestauranteAPI.Repositories.Injections;

namespace RestauranteAPI.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userReposiroty;

        public UserService(IUserRepository userRepository)
        {
            _userReposiroty = userRepository;
        }
        public User GetUser(string user, string password)
        {
            var result = _userReposiroty.GetUserAsync(user, password).Result;
            return result;
        }
    }
}
