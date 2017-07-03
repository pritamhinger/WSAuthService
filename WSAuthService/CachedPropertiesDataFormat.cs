﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSAuthService
{
    public class CachedPropertiesDataFormat : ISecureDataFormat<AuthenticationProperties>
    {
        public const string CacheKeyPrefix = "CachedPropertiesData-";

        private readonly IDistributedCache _cache;
        private readonly IDataProtector _dataProtector;
        private readonly IDataSerializer<AuthenticationProperties> _serializer;

        public CachedPropertiesDataFormat(
            IDistributedCache cache,
            IDataProtector dataProtector)
            : this(cache, dataProtector, new PropertiesSerializer())
        {

        }

        public CachedPropertiesDataFormat(
            IDistributedCache cache,
            IDataProtector dataProtector,
            IDataSerializer<AuthenticationProperties> serializer)
        {
            _dataProtector = dataProtector;
            _cache = cache;
            _serializer = serializer;
        }

        public string Protect(AuthenticationProperties data)
        {
            return Protect(data, null);
        }

        public string Protect(AuthenticationProperties data, string purpose)
        {
            var key = Guid.NewGuid().ToString();
            var cacheKey = $"{CacheKeyPrefix}{key}";
            var serialized = _serializer.Serialize(data);

            // Rather than encrypt the full AuthenticationProperties
            // cache the data and encrypt the key that points to the data
            _cache.Set(cacheKey, serialized);

            return _dataProtector.Protect(key);
        }

        public AuthenticationProperties Unprotect(string protectedText)
        {
            return Unprotect(protectedText, null);
        }

        public AuthenticationProperties Unprotect(string protectedText, string purpose)
        {
            // Decrypt the key and retrieve the data from the cache.
            var key = _dataProtector.Unprotect(protectedText);
            var cacheKey = $"{CacheKeyPrefix}{key}";
            var serialized = _cache.Get(cacheKey);

            return _serializer.Deserialize(serialized);
        }
    }
}
