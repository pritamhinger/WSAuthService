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
    }
}
