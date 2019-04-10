using System.Collections.Generic;
using System.Threading.Tasks;
using TeslaTizen.Models;

namespace TeslaTizen.Data
{
    public interface ITeslaAPIWrapper
    {
        Task<TeslaAuthentication> Login(string email, string password);
        void SetBearerToken(string bearerToken);
        Task<List<TeslaVehicle>> GetVehicles();
        Task<TeslaVehicle> WakeUp(TeslaVehicle vehicle);
        // Commands
        Task<CommandResult> StartAutoConditioning(TeslaVehicle vehicle);
        Task<CommandResult> StopAutoConditioning(TeslaVehicle vehicle);
        Task<CommandResult> SetTemps(TeslaVehicle vehicle, double driverTemp, double passengerTemp);
        Task<CommandResult> SeatHeaterRequest(TeslaVehicle vehicle, int heater, int level);
        Task<CommandResult> SteeringWheelHeaterRequest(TeslaVehicle vehicle, bool on);
        Task<CommandResult> ChargePortOpen(TeslaVehicle vehicle);
        Task<CommandResult> ChargePortClose(TeslaVehicle vehicle);
        Task<CommandResult> ChargeStart(TeslaVehicle vehicle);
        Task<CommandResult> ChargeStop(TeslaVehicle vehicle);
        Task<CommandResult> SetChargeLimit(TeslaVehicle vehicle, int percent);
        Task<CommandResult> DoorUnlock(TeslaVehicle vehicle);
        Task<CommandResult> DoorLock(TeslaVehicle vehicle);
        Task<CommandResult> ActuateTrunk(TeslaVehicle vehicle, string whichTrunk);
        Task<CommandResult> HonkHorn(TeslaVehicle vehicle);
        Task<CommandResult> FlashLights(TeslaVehicle vehicle);
        Task<CommandResult> SunRoofControl(TeslaVehicle vehicle, string state);
    }
}
