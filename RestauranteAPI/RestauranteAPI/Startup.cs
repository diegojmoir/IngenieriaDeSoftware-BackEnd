using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using RestauranteAPI.Configuration.FirebaseConfiguration;
using System;
using System.Threading;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RestauranteAPI.Services.Injections;
using RestauranteAPI.Services;
using RestauranteAPI.Configuration.NinjectConfiguration;
using RestauranteAPI.Models.Mapping;

namespace RestauranteAPI
{
    public class Startup
    {
        private readonly AsyncLocal<Scope> _scopeProvider = new AsyncLocal<Scope>();
        private IKernel Kernel { get; set; }

        private object Resolve(Type type) => Kernel.Get(type);
        private object RequestScope(IContext context) => _scopeProvider.Value;


        private sealed class Scope : DisposableObject
        {
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions (jo =>
                {
                    jo.SerializerSettings.ContractResolver = new DefaultContractResolver();
                }
            );
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRequestScopingMiddleware(() => _scopeProvider.Value = new Scope());
            services.AddCustomControllerActivation(Resolve);
            services.AddCustomViewComponentActivation(Resolve);
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Restaurante_DEV", Version = "v1"}); });

            // configure jwt authentication
            //var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes("THIS IS OUR SECRET FOR THE JWT");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Kernel = RegisterApplicationComponents(app);
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurante_DEV"); });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            FirebaseConfig.SetEnviromentVariables(Configuration);
            app.UseHttpsRedirection();

            // global cors policy
            //app.UseCors(options => options.WithOrigins("http://www.mydomain.com").AllowAnyMethod());
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseMvc();
        }

        private IKernel RegisterApplicationComponents(IApplicationBuilder app)
        {
            // IKernelConfiguration config = new KernelConfiguration();
            var kernel = new StandardKernel();

            // Register application services
            foreach (var ctrlType in app.GetControllerTypes())
            {
                kernel.Bind(ctrlType).ToSelf().InScope(RequestScope);
            }
            // This is where our bindings are configured
            NinjectWebBindingConfigure.BindClasses(kernel, RequestScope);
            // Cross-wire required framework services
            kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);

            return kernel;
        }
    }
}