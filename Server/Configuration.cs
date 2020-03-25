using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Server
{
    public static class Configuration
    {
        // New
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
    
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("dummy_api", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "console_app",
                    ClientSecrets = { new Secret("console_app_secret".ToSha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "dummy_api" }
                },
                // New
                new Client
                {
                    ClientId = "mvc_client",
                    ClientSecrets = { new Secret("mvc_client_secret".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    AllowedScopes =
                    {
                        "dummy_api",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    RedirectUris = { "https://localhost:44333/signin-oidc" }
                }
            };
    }
}
