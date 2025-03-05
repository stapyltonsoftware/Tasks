using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Core.Application.ThirdPartyServices;
using Tasks.Infrastructure.OpenAIServices.Implementations;

namespace Tasks.Infrastructure.OpenAIServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOpenAIServices(this IServiceCollection services) 
        {
            services.AddScoped<IToDoAIService, OpenAIToDoService>();

            return services;
        } 
    }
}
