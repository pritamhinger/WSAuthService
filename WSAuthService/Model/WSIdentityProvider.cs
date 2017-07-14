using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSAuthService.Model
{
    public class WSIdentityProvider
    {
        public long Id { get; set; }
        public string AuthorityURL { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
    }
}
