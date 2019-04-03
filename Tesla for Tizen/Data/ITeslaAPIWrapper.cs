using System.Collections.Generic;
using TeslaTizen.Models;

namespace TeslaTizen.Data
{
    public interface ITeslaAPIWrapper
    {
        TeslaAuthentication Login(string email, string password);
        void SetBearerToken(string bearerToken);
        List<TeslaVehicle> GetVehicles();
    }
}
