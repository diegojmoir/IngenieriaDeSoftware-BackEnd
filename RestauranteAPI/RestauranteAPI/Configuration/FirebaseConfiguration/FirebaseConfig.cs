using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Firebase.Database;


namespace RestauranteAPI.Configuration.FirebaseConfiguration
{

    public  static class FirebaseConfig
    {
        public static FirebaseClient FirebaseClient;
        public  static string ApiKey { get; private set; }
        public static string AuthEmail { get; private set; }
        public static string AuthPassword { get; private set; }
        public static string Url { get; private set; }

        public static void SetEnviromentVariables(IConfiguration configuration) 
        {
            ApiKey = configuration.GetSection("FirebaseConfig").GetValue<string>("apiKey");
            AuthEmail = configuration.GetSection("FirebaseConfig").GetValue<string>("AuthEmail");
            AuthPassword = configuration.GetSection("FirebaseConfig").GetValue<string>("AuthPassword");
            Url = configuration.GetSection("FirebaseConfig").GetValue<string>("databaseURL");
        }

        public static async Task FirebaseStartUp() 
        {
          

            var firebaseAuth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig (ApiKey));
            var firebaseSignIn = await firebaseAuth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            var client = new FirebaseClient(Url,
                new FirebaseOptions()
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(firebaseSignIn.FirebaseToken)
                });
            FirebaseClient = client;
        }
    }
}
