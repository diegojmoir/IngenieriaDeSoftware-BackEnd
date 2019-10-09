namespace RestauranteAPI.Services.Injections
{
    using RestauranteAPI.Models;
    public interface IUserService
    {
        User GetUser(string user, string password);
        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
        User CreateUser(User user); 
    }
}
