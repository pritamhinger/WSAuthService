using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WSAuthService.DataBase;
using WSAuthService.Model;

namespace WSAuthService.Controllers
{
	[Route("IDP")]
	public class IDPController : Controller
	{
		public object DBLayer { get; private set; }
		DBlayer dbLayer = new DBlayer();
		public IActionResult Index()
		{
			return View();
		}

		#region IdentiTy Provider Actions
		[HttpGet]
		public IActionResult GetIdentityProviderForTenantId(long tenantId)
		{
			List<WSIdentityProvider> identityProviders = dbLayer.getAllIDPs(tenantId);
			return Ok(new { identityProviders });

		}

		[HttpGet("{id}")]
		public IActionResult GetIdentityProviderForId(long Id)
		{
			WSIdentityProvider identityProvider = TestData.GetIdentityProviderById(Id);
			return CreatedAtRoute("IDP", new { id = identityProvider.Id }, identityProvider);

		}
		[HttpPost]
		public IActionResult Create([FromBody] WSIdentityProvider identityProvider, long tenantId)
		{
			if (identityProvider == null )
			{
				return BadRequest();
			}

			// TODO: Save the IDP here to DB
			DBlayer dbLayer = new DBlayer();
			identityProvider.TenantId = tenantId;
			dbLayer.CreateIDP(identityProvider);

			//return CreatedAtRoute("IDP", new { id = identityProvider.Id }, identityProvider);
			return Ok(new { identityProvider });
		}

		[HttpPut("idp/{idpId}")]
		public IActionResult UpdateIdentityProvider(long id, long idpId, [FromBody] WSIdentityProvider identityProvider)
		{
			if (identityProvider == null || idpId != identityProvider.Id)
			{
				return BadRequest();
			}

			//var tenant = TestData.GetTenantById(id);
			//if (tenant == null || tenant.IdentityProvider.Id != idpId)
			//{
			//	return BadRequest();
			//}

			// TODO:  Update IdentityProvider here
			return new NoContentResult();
		}

		[HttpDelete("{id}/idp/{idpId}")]
		public IActionResult DeleteIdentityProvider(long id, long idpId, [FromBody] WSIdentityProvider identityProvider)
		{
			if (identityProvider == null || idpId != identityProvider.Id)
			{
				return BadRequest();
			}

			//var tenant = TestData.GetTenantById(id);
			//if (tenant == null || tenant.IdentityProvider.Id != idpId)
			//{
			//	return BadRequest();
			//}

			// TODO:  Delete IdentityProvider here
			return new NoContentResult();
		}
		#endregion

	}
}