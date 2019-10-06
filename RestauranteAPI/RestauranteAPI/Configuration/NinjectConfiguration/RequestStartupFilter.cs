using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteAPI
{
    public sealed class RequestStartupFilter : IStartupFilter
    {
        private readonly Func<IDisposable> requestScopeProvider;

        public RequestStartupFilter(Func<IDisposable> requestScopeProvider)
        {
            this.requestScopeProvider = requestScopeProvider ?? throw new ArgumentNullException(nameof(requestScopeProvider));
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                ConfigureRequestScoping(builder);
                next(builder);
            };
        }

        private void ConfigureRequestScoping(IApplicationBuilder builder)
        {
            builder.Use(async (context, next) =>
            {
                using (var scope = this.requestScopeProvider())
                {
                    await next();
                }
            });
        }
    }
}
