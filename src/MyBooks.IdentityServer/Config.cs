using System.Collections.Generic;
using IdentityServer4.Models;

namespace MyBooks.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => GetIdentityResources();

        public static IEnumerable<ApiResource> ApiResources => GetApiResources();

        public static IEnumerable<Client> Clients => GetClients();

        private static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        private static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("myBooksApi", "MyBooks API")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) }
                }
            };
        }

        private static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "myBooksApi" }
                },
                // Native clients
                new Client
                {
                    ClientId = "native.code", //"native.hybrid",
                    ClientName = "Native Client (Hybrid with PKCE)",

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },

                    RequireClientSecret = false,
                    RequireConsent = false,

                    AllowedGrantTypes = GrantTypes.Code, //.Hybrid,
                    RequirePkce = true,
                    AllowedScopes = {"openid", "profile", "email", "myBooksApi"},

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };
        }
    }
}