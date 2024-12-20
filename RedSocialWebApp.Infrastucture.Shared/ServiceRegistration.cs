﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocialWebApp.Core.Application.Interfaces.Services;
using RedSocialWebApp.Core.Domain.Settings;
using RedSocialWebApp.Infrastucture.Shared.Services;

namespace RedSocialWebApp.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));         
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
