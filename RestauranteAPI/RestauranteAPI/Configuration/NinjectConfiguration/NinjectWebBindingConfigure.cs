using Ninject;
using RestauranteAPI.Services;
using System;
using RestauranteAPI.Repositories.Injections;
using RestauranteAPI.Services.Injections;
using RestauranteAPI.Repositories;
namespace RestauranteAPI
{
    public static class NinjectWebBindingConfigure
    {
        
        public static void BindClasses(StandardKernel kernel, Func<Ninject.Activation.IContext, object> RequestScope) 
        {
            //repositories
            kernel.Bind<ITestRepository>().To<TestRepository>().InScope(RequestScope);
            kernel.Bind<IUserRepository>().To<UserRepository>().InScope(RequestScope);
            //services
            kernel.Bind<ITestService>().To<TestService>().InScope(RequestScope);
            kernel.Bind<IUserService>().To<UserService>().InScope(RequestScope);
        }
    }
}
