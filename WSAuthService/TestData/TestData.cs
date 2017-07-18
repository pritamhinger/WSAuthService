using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSAuthService.Model;

namespace WSAuthService
{
    public class TestData
    {
        #region Private Variables
   //     internal static Tenant tenant = new Tenant {
   //         Id = 1,
			//TenantIdentifier = new Guid().ToString(),
			//DomainName = "winshuttle.com",
   //         IdentityProvider = new WSIdentityProvider {
   //             Id = 1,
			//	AuthorityURL = "https://www.winshuttle.com",
   //             Metadata = new Dictionary<string, object> {
   //                 { "key1","value1" },
   //                 { "key2",123 }
   //             }
   //         },
   //         Users = new List<User>() {
   //                new User {
   //                    Id = 1,
   //                    UserIdentifier = "pHinger"
   //                },
   //                new User {
   //                    Id = 2,
   //                    UserIdentifier = "dVerma"
   //                },
   //                new User {
   //                    Id = 3,
   //                    UserIdentifier = "pKaur"
   //                }
   //         }
   //     };
		internal static Tenant tenant = new Tenant
		{
			Id = 1,
			TenantIdentifier = new Guid().ToString(),
			DomainName = "winshuttle.com",
			IdentityProvider = new WSIdentityProvider
			{
				Id = 1,
				AuthorityURL = "https://www.winshuttle.com",
				Metadata = new Dictionary<string, object> {
					{ "key1","value1" },
					{ "key2",123 }
				}
			},
			Users = new List<User>() {
				   new User {
					   Id = 1,
					   UserIdentifier = "pHinger"
				   },
				   new User {
					   Id = 2,
					   UserIdentifier = "dVerma"
				   },
				   new User {
					   Id = 3,
					   UserIdentifier = "pKaur"
				   }
			}
		};

		static List<User> users = new List<User>() {
            new User {
                       Id = 1,
                       UserIdentifier = "pHinger"
                   },
                   new User {
                       Id = 2,
                       UserIdentifier = "dVerma"
                   },
                   new User {
                       Id = 3,
                       UserIdentifier = "pKaur"
                   }
        };

        static WSIdentityProvider idp = new WSIdentityProvider {
            Id = 1,
            AuthorityURL = "https://www.winshuttle.com",
            Metadata = new Dictionary<string, object> {
                { "key1","value1" },
                { "key2",123 }
            }
		};
        #endregion

        public static Tenant GetTenantById(long id)
        {
            // TODO: Call your network layer here.
            if (id == tenant.Id) {
                return tenant;
            }

            return null;
        }

		public static WSIdentityProvider GetIdentityProviderById(long id)
		{
			// TODO: Call your network layer here.
			if (id == idp.Id)
			{
				return idp;
			}

			return null;
		}
	}
}
