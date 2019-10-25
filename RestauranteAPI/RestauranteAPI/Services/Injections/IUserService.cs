using RestauranteAPI.Models.Dto;

namespace RestauranteAPI.Services.Injections
{
    using RestauranteAPI.Models;
    public interface IUserService
    {
        UserDto GetUser(string user, string password);
        bool CheckUserAlreadyExist(string username, string email);
        UserDto GetUserByEmail(string email);
        UserDto CreateUser(User user); 
        UserDto Authenticate(string username, string password);
    }
}
