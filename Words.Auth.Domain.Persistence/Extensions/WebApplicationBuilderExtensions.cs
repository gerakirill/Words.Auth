namespace Words.Auth.Domain.Persistence.Extensions;

using Models;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Context;
using System.Security.Cryptography.X509Certificates;
using Options;

public static class WebApplicationBuilderExtensions
{
    public static void AddUserContext(this WebApplicationBuilder builder)
    {
        string? assembly = Assembly.GetExecutingAssembly().GetName().Name;
        string connectionString = builder.Configuration.GetConnectionString("UserContext");

        builder.Services.AddDbContext<UserContext>(options =>
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(assembly)));
    }

    public static void AddUserIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<GameUser, IdentityRole>()
            .AddEntityFrameworkStores<UserContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddIdentityProvider(this WebApplicationBuilder builder, X509Certificate2 signInDocument)
    {

        var identityOptions = new IdentityConfigurationOptions();
        builder.Configuration
            .GetSection(IdentityConfigurationOptions.IdentityConfigurationName)
            .Bind(identityOptions);

        builder.Services.AddIdentityServer()
            .AddInMemoryCaching()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator<GameUser>>()
            .AddInMemoryApiScopes(identityOptions.MapScopes())
            .AddInMemoryApiResources(identityOptions.MapApiResources())
            .AddInMemoryClients(identityOptions.MapClients())
            .AddResourceStore<InMemoryResourcesStore>()
            .AddInMemoryIdentityResources(identityOptions.MapResources())
            .AddSigningCredential(signInDocument)
            .AddProfileService<IdentityManager.IdentityManager>();
    }

    public static void UseIdentityProvider(this IApplicationBuilder app)
    {
        app.UseIdentityServer();
    }
}