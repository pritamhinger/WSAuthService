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

        [HttpPut("{id}/idp/{idpId}")]
        public IActionResult UpdateIdentityProvider(long id, long idpId, [FromBody] WSIdentityProvider identityProvider)
        {
            if(identityProvider == null || idpId == identityProvider.Id) {
                return BadRequest();
            }

            var tenant = GetTenantById(id);
            if (tenant.IdentityProvider.Id != idpId){
                return BadRequest();
            }

            // TODO:  Update IdentityProvider here
            return new NoContentResult();
        }

        [HttpPut("{id}/idp/{idpId}")]
        public IActionResult DeleteIdentityProvider(long id, long idpId, [FromBody] WSIdentityProvider identityProvider)
        {
            if (identityProvider == null || idpId == identityProvider.Id) {
                return BadRequest();
            }

            var tenant = GetTenantById(id);
            if (tenant.IdentityProvider.Id != idpId) {
                return BadRequest();
            }

            // TODO:  Delete IdentityProvider here
            return new NoContentResult();
        }

        [HttpPost("{tenantId}/user")]
        public IActionResult CreateUser(long tenantId, [FromBody] User user)
        {
            if(user == null) {
                return BadRequest();
            }

            var tenant = GetTenantById(tenantId);
            if(tenant == null) {
                return BadRequest();
            }

            // TODO: Create User for Tenant
            return CreatedAtRoute("{id}/user{userId}",  new { id = tenantId, userId = user.Id }, user);
        }

        [HttpPut("{id}/user/{userId}")]
        public IActionResult UpdateUser(long id, long userId, [FromBody] User user)
        {
            if (user == null || user.Id != userId) {
                return BadRequest();
            }

            var tenant = GetTenantById(id);
            var userRef = tenant.Users.Where(tempUser => tempUser.Id == userId).First<User>();
            if (userRef == null) {
                return BadRequest();
            }

            // TODO:  Update User here
            return new NoContentResult();
        }

        [HttpPut("{id}/user/{userId}")]
        public IActionResult DeleteUser(long id, long userId, [FromBody] User user)
        {
            if (user == null || user.Id != userId) {
                return BadRequest();
            }

            var tenant = GetTenantById(id);
            var userRef = tenant.Users.Where(tempUser => tempUser.Id == userId).First<User>();
            if (userRef == null) {
                return BadRequest();
            }

            // TODO:  Delete User here
            return new NoContentResult();
        }

        private Tenant GetTenantById(long id)
        {
            return new Tenant();
        }
    }
}