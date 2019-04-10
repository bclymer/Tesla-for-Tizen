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
        private readonly ITeslaCache cache;
        private readonly ITeslaAPIWrapper teslaAPI;

        public TeslaService(ITeslaCache cache, ITeslaAPIWrapper teslaAPI)
        {
            this.cache = cache;
            this.teslaAPI = teslaAPI;
            var cachedAuth = cache.GetAuthentication();
            if (cachedAuth != null)
            {
                teslaAPI.SetBearerToken(cachedAuth.AccessToken);
            }
        }

        public bool RequiresLogin()
        {
            return cache.GetAuthentication() == null;
        }

        public async Task Login(string email, string password)
        {
            var auth = await teslaAPI.Login(email, password);
            cache.StoreAuthentication(auth);
        }

        public async Task<List<TeslaVehicle>> GetVehicles(bool forceRefresh = false)
        {
            var vehicles = await cache.GetVehicles();
            if (forceRefresh || vehicles == null)
            {
                vehicles = await teslaAPI.GetVehicles();
                cache.StoreVehicles(vehicles);
            }
            return vehicles ?? new List<TeslaVehicle>();
        }
    }
}
