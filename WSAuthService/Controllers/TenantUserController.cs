using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WSAuthService.Model;

namespace WSAuthService.ProjectController
{
	[Route("user")]
	public class TenantUserController : Controller
	{

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
	}
}