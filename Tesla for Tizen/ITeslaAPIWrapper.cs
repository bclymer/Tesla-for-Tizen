using System.Collections.Generic;
using Tesla_for_Tizen.Models;
using TeslaTizen.Models;

namespace Tesla_for_Tizen
{
    public interface ITeslaAPIWrapper
    {
        TeslaAuthentication Login(string email, string password);
        void SetBearerToken(string bearerToken);
        List<TeslaVehicle> GetVehicles();
    }
}
