using System.Collections.Generic;
using RestauranteAPI.Models;
using RestauranteAPI.Repositories.Injections;
using Firebase.Database;
using RestauranteAPI.Configuration.FirebaseConfiguration;
using Firebase.Database.Query;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;
using RestauranteAPI.Configuration.Scaffolding;

namespace RestauranteAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected RestauranteDbContext Context { get; set; }

        public UserRepository(RestauranteDbContext context)
        {
            Context = context;
        }

        public User CreateUserInStorage(User user)
        {
            Context.Users.Add(user);
                Context.SaveChanges();
                return user;
        }

        public List<User> GetExistentUsers(string username,string email)
        {
            var result = Context.Users
                .Where(x => x.Username == username||x.Email==email)
                .ToList();//When the query is enumerated, then is sent to db
                return result;
        }

        public FirebaseObject<User> GetUserFromStorageByEmail(string email)
        {
            FirebaseConfig.FirebaseStartUp().Wait();
            using (var client = FirebaseConfig.FirebaseClient)
            {
                var response = client
                    .Child("UsersCollection")
                    .Child("Users")
                    .OnceAsync<User>()
                    .Result
                    .FirstOrDefault(x => x.Object != null && (x.Object.Email == email));
                return response;
            }
        }

        public User GetUserFromStorageByUserNameAndPassword(string user, string password)
        {
            var result=Context.Users
                .FirstOrDefault(x => x.Username == user && x.Password == password);
            return result;
        }

        public string CreateToken(string username)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("THIS IS OUR SECRET FOR THE JWT");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var Token = tokenHandler.WriteToken(token);
            return Token;
        }
    }
}
