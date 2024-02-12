using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Words.Auth.Infrastructure
{
    public static class WebApplicationBuilderExtensions
    {
        public static void UseLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));
        }
    }
}
