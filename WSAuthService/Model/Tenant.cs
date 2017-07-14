using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSAuthService.Model
{
    public class Tenant
    {
        public long Id { get; set; }
        public string DomainName { get; set; }
        public WSIdentityProvider IdentityProvider { get; set; }
    }
}
