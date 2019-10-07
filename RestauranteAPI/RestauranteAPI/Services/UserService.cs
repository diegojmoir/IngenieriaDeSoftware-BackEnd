using RestauranteAPI.Models;
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

        public User CreateUser(User user)
        {
            var resultObject = _userReposiroty.CreateUserInStorage(user);
            if (resultObject ==null)
                return null;
            var result = resultObject.Object;
            result.Key = resultObject.Key;
            return result;
        }

        public User GetUser(string user, string password)
        {
            var resultObject = _userReposiroty
                .GetUserFromStorageByUserNameAndPassword(user, password);
            if(resultObject==null)
                return null;
            var result = resultObject.Object;
            result.Key = resultObject.Key;
            return result;
        }
    }
}
