using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using WSAuthService.Model;

namespace WSAuthService.ProjectController
{
    [Route("tenant")]
    [Authorize]
    public class TenantController : Controller
    {
        [HttpPost]
        public IActionResult Create([FromBody] Tenant tenant)
        {
            if (tenant == null) {
                return BadRequest();
            }

            // TODO: Save the tenant here to DB

            return CreatedAtRoute("tenant", new { id = tenant.Id }, tenant);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Tenant tenant)
        {
            if (tenant == null || tenant.Id != id) {
                return BadRequest();
            }

            // TODO :  Get the Tenant Based on Id

            // TODO : Update the tenant referance with new values
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            // TODO : Get the tenant based on Id

            // var tenant = GetTheTenant(id)
            // if (tenant == null) {
            //      return NotFound();
            //}

            // TODO: Remove the tenant
            return new NoContentResult();
        }

        [HttpPut("{id}/idp")]
        public IActionResult UpdateIdentityProvider(long id, [FromBody] WSIdentityProvider identityProvider)
        {
            // Get Tenant from id
            // if tenant.IdentityProvider.Id != identityProvider.Id{
            //      return BadRequest()
            //}

            // TODO:  Update IdentityProvider here
            return new NoContentResult();
        }

        [HttpPut("{id}/idp/{idpID}")]
        public IActionResult DeleteIdentityProvider(long id, long idpID, [FromBody] WSIdentityProvider identityProvider)
        {
            // TODO: Get Tenant from id
            // if tenant.IdentityProvider.Id != identityProvider.Id{
            //      return BadRequest()
            //}

            // TODO:  Delete IdentityProvider here
            return new NoContentResult();
        }
    }
}