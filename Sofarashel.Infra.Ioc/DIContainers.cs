using Microsoft.Extensions.DependencyInjection;
using Sofarashel.Application.Services.Implementation;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Data.Contract;
using Sofarashel.Data.Implementation;
using System;
using System.Collections.Generic;
using System.Text;


namespace Sofarashel.Infra.Ioc
{
    public static class DIContainers
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Services
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IAccountServices, AccountServices>();
            #endregion
        }
    }
}
