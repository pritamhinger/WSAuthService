using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using WSAuthService.Model;
using WSAuthService.DataBase;

namespace WSAuthService.ProjectController
{
    [Route("tenant")]
    //[Authorize]
    public class TenantController : Controller
    {
        #region Tenant Actions
        [HttpGet("{id}")]
        public IActionResult GetTenantForId(long id)
        {
            Tenant newtenant = TestData.GetTenantById(id);
            return CreatedAtRoute("tenant", new { id = newtenant.Id }, newtenant);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tenant tenant)
        {
            if (tenant == null) {
                return BadRequest();
            }

			// TODO: Save the tenant here to DB
			DBlayer dbLayer = new DBlayer();
			tenant=dbLayer.Create(tenant);
           // tenant = TestData.tenant;
            return CreatedAtRoute("tenant", new { id = tenant.Id }, tenant);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Tenant tenant)
        {
            if (tenant == null || tenant.Id != id) {
                return BadRequest();
            }

            // TODO :  Get the Tenant Based on Id
            tenant = TestData.GetTenantById(id);
            if (tenant == null) {
                return BadRequest();
            }

            // TODO : Update the tenant referance with new values
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Tenant tenant = TestData.GetTenantById(id);
            if (tenant == null) {
                return BadRequest();
            }

            // TODO: Remove the tenant
            return new NoContentResult();
        }
        #endregion

        #region User Actions

        [HttpGet("{id}/user/{userId}", Name = "getuser")]
        public IActionResult GetUserForTenantId(long id, long userId)
        {
            Tenant tenant = TestData.GetTenantById(id);
            if (tenant == null) {
                return BadRequest();
            }

            User user = tenant.Users.Where(usr => usr.Id == userId).First<User>();
            if (user == null) {
                return NotFound();
            }

            return Ok(new { results = user });
        }

        [HttpPost("{tenantId}/user")]
        public IActionResult CreateUser(long tenantId, [FromBody] User user)
        {
            if (user == null) {
                return BadRequest();
            }

            var tenant = TestData.GetTenantById(tenantId);
            if (tenant == null) {
                return BadRequest();
            }

            // TODO: Create User for Tenant
            //var obj = CreatedAtRoute("{id}/user/{userId}",  new { id = tenantId, userId = user.Id }, user);
            return CreatedAtRoute(routeName: "getuser", routeValues: new { id = tenantId, userId = user.Id }, value: user);
        }

        [HttpPut("{id}/user/{userId}")]
        public IActionResult UpdateUser(long id, long userId, [FromBody] User user)
        {
            if (user == null || user.Id != userId) {
                return BadRequest();
            }

            var tenant = TestData.GetTenantById(id);
            if (tenant == null) {
                return BadRequest();
            }

            var userRef = tenant.Users.Where(tempUser => tempUser.Id == userId).First<User>();
            if (userRef == null) {
                return BadRequest();
            }

            // TODO:  Update User here
            return new NoContentResult();
        }

        [HttpDelete("{id}/user/{userId}")]
        public IActionResult DeleteUser(long id, long userId, [FromBody] User user)
        {
            if (user == null || user.Id != userId) {
                return BadRequest();
            }

            var tenant = TestData.GetTenantById(id);
            if (tenant == null) {
                return BadRequest();
            }

            var userRef = tenant.Users.Where(tempUser => tempUser.Id == userId).First<User>();
            if (userRef == null) {
                return BadRequest();
            }

            // TODO:  Delete User here
            return new NoContentResult();
        }
        #endregion
    }
}