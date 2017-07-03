using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSAuthService
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResource() {
            return new List<ApiResource>{
                new ApiResource("api1","Sample API Provider")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                new Client {
                    ClientId = "2beaf9f4-9eeb-4219-983e-ae8e454b70e6",
                    ClientName = "Winshuttle ADMApp 2.0",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://admappwebsite.azurewebsites.net/" },
                    PostLogoutRedirectUris = { "https://admappwebsite.azurewebsites.net/" },
                    AllowedScopes = new List<string> {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser> {
                new TestUser {
                    SubjectId =  Guid.NewGuid().ToString(),
                    Username = "pritamh",
                    Password = "$abcd1234"
                },
                new TestUser {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "phinger",
                    Password = "$abcd1234"
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }
    }
}
