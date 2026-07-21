using Bibaket.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sofarashel.Application.Services.Implementation;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Data.Contract;
using Sofarashel.Data.Implementation;
using Sofarashel.Domain.Contracts;
using Sofarashel.Infra.Data.Repositories;
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
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            #endregion

            #region Services
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IRoleServices, RoleServices>();
            services.AddScoped<IPermissionService, PermissionService>();


            #endregion
        }
    }
}
