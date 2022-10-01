using Microsoft.Extensions.DependencyInjection;
using MiniSalesApp.Data;
using MiniSalesApp.Application.InterFaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniSalesApp
{
    public static class ContextServiceExtensions
    {
        public static IServiceCollection AddMiniSalesAppContext(this IServiceCollection services)
        {
            services.AddTransient<IMiniSalesAppContext, MiniSalesAppContext>();
            return services;
        }
    }
}
