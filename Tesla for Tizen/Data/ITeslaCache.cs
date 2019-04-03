using System.Collections.Generic;
using TeslaTizen.Models;

namespace TeslaTizen.Data
{
    public interface ITeslaCache
    {
        void StoreAuthentication(TeslaAuthentication auth);
        TeslaAuthentication GetAuthentication();
        void StoreVehicles(List<TeslaVehicle> vehicles);
        List<TeslaVehicle> GetVehicles();
    }
}
