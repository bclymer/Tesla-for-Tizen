using System.Collections.Generic;
using System.Threading.Tasks;
using TeslaTizen.Data;
using TeslaTizen.Models;

namespace TeslaTizen.Services
{
    public class TeslaService
    {
        // cache provider
        // tesla api
        private readonly ITeslaCache Cache;
        private readonly ITeslaAPIWrapper TeslaAPI;

        public TeslaService(ITeslaCache cache, ITeslaAPIWrapper teslaAPI)
        {
            Cache = cache;
            TeslaAPI = teslaAPI;
            var cachedAuth = cache.GetAuthentication();
            if (cachedAuth != null)
            {
                teslaAPI.SetBearerToken(cachedAuth.AccessToken);
            }
        }

        public bool RequiresLogin()
        {
            return Cache.GetAuthentication() == null;
        }

        public async Task Login(string email, string password)
        {
            var auth = await TeslaAPI.Login(email, password);
            TeslaAPI.SetBearerToken(auth.AccessToken);
            Cache.StoreAuthentication(auth);
        }

        public async Task<List<TeslaVehicle>> GetVehicles(bool forceRefresh = false)
        {
            var vehicles = await Cache.GetVehicles();
            if (forceRefresh || vehicles == null)
            {
                vehicles = await TeslaAPI.GetVehicles();
                Cache.StoreVehicles(vehicles);
            }
            return vehicles ?? new List<TeslaVehicle>();
        }
    }
}
