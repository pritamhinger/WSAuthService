using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WSAuthService.Model;
using Microsoft.EntityFrameworkCore;

namespace WSAuthService.DataBase
{
    public class DBlayer
    {
		IMongoClient _client;
		IMongoDatabase _database;
		string connectionString;

		public DBlayer()
		{
			connectionString = "mongodb://wil120:27017";
			_client = new MongoClient(connectionString);
			_database = _client.GetDatabase("Authentication");
		}

		public Tenant Create(Tenant tenant)
		{

			_database.GetCollection<Tenant>("Tenant").InsertOne(tenant);
				return tenant;
		}
		public long CreateUser(User user)
		{
			_database.GetCollection<User>("User").InsertOne(user);
			return user.Id;
		}

		public WSIdentityProvider CreateIDP(WSIdentityProvider wsIdentityProvider)
		{
			_database.GetCollection<WSIdentityProvider>("WSIdentityProviders").InsertOne(wsIdentityProvider);
			return wsIdentityProvider;
		}

		public List<WSIdentityProvider> getAllIDPs(long TenantId)
		{

			List<WSIdentityProvider> WSIdenttitiyProviders= _database.GetCollection<WSIdentityProvider>("WSIdentityProviders").Find(d=>d.TenantId==TenantId).ToList();
			return WSIdenttitiyProviders;
		}


	}
}
