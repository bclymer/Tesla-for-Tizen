using System.Collections.Generic;
using System.Threading.Tasks;
using TeslaTizen.Models;
using TeslaTizen.Utils;

namespace TeslaTizen.Data
{
    public class TeslaCache : ITeslaCache
    {
        private readonly ICache Cache;
        private static readonly string AuthKey = "authKey";
        private static readonly string VehiclesKey = "vehiclesKey";

        public TeslaCache() : this(CacheFactory.CreateCache()) { }

        public TeslaCache(ICache cache)
        {
            Cache = cache;
        }

        public void StoreAuthentication(TeslaAuthentication auth)
        {
            Cache.StoreDataAsync(auth, AuthKey);
        }

        public TeslaAuthentication GetAuthentication()
        {
            return FakeAuth();
            //return Cache.GetData<TeslaAuthentication>(AuthKey);
        }

        public void StoreVehicles(List<TeslaVehicle> vehicles)
        {
            Cache.StoreDataAsync(vehicles, VehiclesKey);
        }

        public async Task<List<TeslaVehicle>> GetVehicles()
        {
            //return new List<TeslaVehicle>
            //{
            //    new TeslaVehicle
            //    {
            //        Name = "Appa",
            //        Id = 12345,
            //    },
            //    new TeslaVehicle
            //    {
            //        Name = "Jenny",
            //        Id = 54321,
            //    }
            //};
            return await Cache.GetDataAsync<List<TeslaVehicle>>(VehiclesKey);
        }

        private TeslaAuthentication FakeAuth()
        {
            var developer = LocalFileParser.GetDeveloper();
            return new TeslaAuthentication(
                accessToken: developer.AccessToken,
                tokenType: developer.TokenType,
                expiresIn: developer.ExpiresIn,
                createdAt: developer.CreatedAt,
                refreshToken: developer.RefreshToken
            );
        } 
    }
}
