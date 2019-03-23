using System.Collections.Generic;
using TeslaTizen.Models;

namespace Tesla_for_Tizen
{
    public class TeslaService
    {
        // cache provider
        // tesla api
        private readonly ITeslaCache cache;
        private readonly ITeslaAPIWrapper teslaAPI;

        public TeslaService() : this(new TeslaCache(), new TeslaAPIWrapper()) { }

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

        public void Login(string email, string password)
        {
            var auth = teslaAPI.Login(email, password);
            cache.StoreAuthentication(auth);
        }

        public List<TeslaVehicle> GetVehicles(bool forceRefresh = false)
        {
            var vehicles = cache.GetVehicles();
            if (forceRefresh || vehicles == null)
            {
                vehicles = teslaAPI.GetVehicles();
                cache.StoreVehicles(vehicles);
            }
            return vehicles ?? new List<TeslaVehicle>();
        }
    }
}
