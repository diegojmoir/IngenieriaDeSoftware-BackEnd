using AutoMapper;
using RestauranteAPI.Models;
using RestauranteAPI.Models.Dto;
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
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDto CreateUser(User user)
        {
            var resultObject = _userRepository.CreateUserInStorage(user);
            if (resultObject ==null)
                return null;
            var result=new UserDto();
            result = _mapper.Map( resultObject,result);
            return result;
        }

        public UserDto GetUser(string user, string password)
        {
            var resultObject = _userRepository
                .GetUserFromStorageByUserNameAndPassword(user, password);
            if(resultObject==null)
                return null;
            var result=new UserDto();
            result = _mapper.Map( resultObject,result);
            return result;
        }

        public UserDto GetUserByUsername(string username)
        {
            var resultObject = _userRepository
                .GetUserFromStorageByUsername(username);
            if(resultObject == null)
                return null;

            var result=new UserDto();
            result = _mapper.Map( resultObject,result);
            return result;
        }

        public UserDto GetUserByEmail(string email)
        {
            var resultObject = _userRepository
                .GetUserFromStorageByEmail(email);
            if (resultObject == null)
                return null;
            var result=new UserDto();
            result = _mapper.Map( resultObject,result);
            return result;
        }

        public UserDto Authenticate(string username, string password)
        {
            var resultObject = _userRepository.GetUserFromStorageByUserNameAndPassword(username, password);
            UserDto user = new UserDto();
            user = _mapper.Map(resultObject, user);
            if (resultObject != null)
            {
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
