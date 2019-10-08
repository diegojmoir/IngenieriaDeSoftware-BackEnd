using RestauranteAPI.Models;
using RestauranteAPI.Services.Injections;
using RestauranteAPI.Repositories.Injections;

namespace RestauranteAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

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
    }
}
