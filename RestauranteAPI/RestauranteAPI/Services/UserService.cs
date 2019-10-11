using RestauranteAPI.Models;
using RestauranteAPI.Services.Injections;
using RestauranteAPI.Repositories.Injections;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using RestauranteAPI.Helpers;
using Microsoft.Extensions.Options;

namespace RestauranteAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User CreateUser(User user)
        {
            var resultObject = _userRepository.CreateUserInStorage(user);
            if (resultObject ==null)
                return null;
            var result = resultObject.Object;
            result.Key = resultObject.Key;
            return result;
        }

        public User GetUser(string user, string password)
        {
            var resultObject = _userRepository
                .GetUserFromStorageByUserNameAndPassword(user, password);
            if(resultObject==null)
                return null;
            var result = resultObject.Object;
            result.Key = resultObject.Key;
            return result;
        }

        public User GetUserByUsername(string username)
        {
            var resultObject = _userRepository
                .GetUserFromStorageByUsername(username);
            if(resultObject == null)
                return null;

            var result = resultObject.Object;
            result.Key = resultObject.Key;
            return result;
        }

        public User GetUserByEmail(string email)
        {
            var resultObject = _userRepository
                .GetUserFromStorageByUsername(email);
            if (resultObject == null)
                return null;

            var result = resultObject.Object;
            result.Key = resultObject.Key;
            return result;
        }

        public User Authenticate(string username, string password)
        {
            User user = null;
            var resultObject = _userRepository.GetUserFromStorageByUserNameAndPassword(username, password);
            if (resultObject != null)
            {
                user = resultObject.Object;
                user.Key = resultObject.Key;
            }

            // return null if user not found
            if (user == null)
                return null;

            user.Token = _userRepository.CreateToken(username);

            // remove password before returning
            user.Password = null;

            return user;
        }
    }
}
