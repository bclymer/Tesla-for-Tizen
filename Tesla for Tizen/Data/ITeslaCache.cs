using System.Collections.Generic;
using System.Threading.Tasks;
using TeslaTizen.Models;

namespace TeslaTizen.Data
{
    public interface ITeslaCache
    {
        void StoreAuthentication(TeslaAuthentication auth);
        TeslaAuthentication GetAuthentication();
        void StoreVehicles(List<TeslaVehicle> vehicles);
        Task<List<TeslaVehicle>> GetVehicles();
    }
}
