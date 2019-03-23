using System.Collections.Generic;
using Tesla_for_Tizen.Models;
using TeslaTizen.Models;

namespace Tesla_for_Tizen
{
    public interface ITeslaCache
    {
        void StoreAuthentication(TeslaAuthentication auth);
        TeslaAuthentication GetAuthentication();
        void StoreVehicles(List<TeslaVehicle> vehicles);
        List<TeslaVehicle> GetVehicles();
    }
}
