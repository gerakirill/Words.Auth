using IdentityServer4.Models;
using Words.Auth.Domain.Persistence.Options;
using ApiResource = IdentityServer4.Models.ApiResource;
using Client = IdentityServer4.Models.Client;

namespace Words.Auth.Domain.Persistence.Extensions
{
    public static class IdentityConfigurationExtensions
    {
        public static ApiScope[] MapScopes(this IdentityConfigurationOptions identityOptions)
        {
            return identityOptions.Scopes
                .Select(x => new ApiScope
                {
                    Name = x,
                    
                })
                .ToArray();
        }

        public static IdentityResource[] MapResources(this IdentityConfigurationOptions identityOptions)
        {
            return identityOptions.Resources.Select(resource => resource switch
                {
                    "openId" => new IdentityResources.OpenId(),
                    _ => throw new ArgumentOutOfRangeException()
                })
                .Cast<IdentityResource>()
                .ToArray();
        }

        public static Client[] MapClients(this IdentityConfigurationOptions identityOptions)
        {
            return identityOptions.Clients
                .Select(x => new Client
                {
                    ClientId = x.ClientId,
                    ClientName = x.Name,
                    AllowedGrantTypes = ToGrantTypes(x.AllowedGrantTypes),
                    AllowedScopes = x.AllowedScopes,
                    RequireClientSecret = x.RequireClientSecret,
                    ClientSecrets = ToSecrets(x.ClientSecrets)
                })
                .ToArray();
        }

        public static ApiResource[] MapApiResources(this IdentityConfigurationOptions identityOptions)
        {
            return identityOptions.ApiResources
                .Select(x => new ApiResource
                {
                    Name = x.Name,
                    Scopes = x.AllowedScopes
                })
                .ToArray();
        }

        private static List<string> ToGrantTypes(string[] grantTypes)
        {
            var result = new List<string>();
            foreach (string grant in grantTypes)
            {
                List<string> entry = grant switch
                {
                    "resourceOwnerPassword" => GrantTypes.ResourceOwnerPassword.ToList(),
                    _ => throw new ArgumentOutOfRangeException()
                };
                result.AddRange(entry);
            }
            return result;
        }

        private static List<Secret> ToSecrets(string[] secrets)
        {
            return secrets
                .Select(x => new Secret(x.Sha256()))
                .ToList();
        }
    }
}
