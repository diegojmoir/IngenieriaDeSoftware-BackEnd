using RestauranteAPI.Models.Dto;

namespace RestauranteAPI.Services.Injections
{
    using RestauranteAPI.Models;
    public interface IUserService
    {
        UserDto GetUser(string user, string password);
        UserDto GetUserByUsername(string username);
        UserDto GetUserByEmail(string email);
        UserDto CreateUser(User user); 
    }
}
