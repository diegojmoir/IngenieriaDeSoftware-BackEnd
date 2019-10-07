namespace RestauranteAPI.Services.Injections
{
    using RestauranteAPI.Models;
    public interface IUserService
    {
        User GetUser(string user, string password);
        User CreateUser(User user); 
    }
}
