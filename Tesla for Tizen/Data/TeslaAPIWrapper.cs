using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tesla.NET;
using TeslaTizen.Models;
using TeslaTizen.Utils;

namespace TeslaTizen.Data
{
    public class TeslaAPIWrapper: ITeslaAPIWrapper
    {
        private readonly HttpClient httpClient;
        private ITeslaClient teslaClient;

        public TeslaAPIWrapper() : this(new HttpClient()) { }

        public TeslaAPIWrapper(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            ProductHeaderValue header = new ProductHeaderValue("Tesla_For_Tizen");
            ProductInfoHeaderValue userAgent = new ProductInfoHeaderValue(header);
            httpClient.DefaultRequestHeaders.UserAgent.Add(userAgent);
        }

        public async Task<TeslaAuthentication> Login(string email, string password)
        {
            var clientKeys = LocalFileParser.GetTeslaClient();
            var client = new TeslaAuthClient(httpClient);
            var login = await client.RequestAccessTokenAsync(
                clientId: clientKeys.ClientId,
                clientSecret: clientKeys.ClientSecret,
                email: email,
                password: password);
            return new TeslaAuthentication(login.Data);
        }

        public void SetBearerToken(string bearerToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            teslaClient = new Tesla.NET.TeslaClient(httpClient);
        }

        public async Task<List<TeslaVehicle>> GetVehicles()
        {
            var apiVehicles = await teslaClient.GetVehiclesAsync();
            var vehilces = apiVehicles?.Data.Response.Where(c => c != null).Select(c => TeslaVehicle.From(c)).ToList();
            return vehilces ?? new List<TeslaVehicle>();
        }

        public async Task<TeslaVehicle> WakeUp(TeslaVehicle vehicle)
        {
            var updatedVehicle = await teslaClient.WakeUpAsync(vehicle.Id);
            return TeslaVehicle.From(updatedVehicle.Data.Response);
        }

        public async Task<CommandResult> StartAutoConditioning(TeslaVehicle vehicle)
        {
            var commandResult = await teslaClient.StartAutoConditioning(vehicle.Id);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> StopAutoConditioning(TeslaVehicle vehicle)
        {
            var commandResult = await teslaClient.StopAutoConditioning(vehicle.Id);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> SetTemps(TeslaVehicle vehicle, double driverTemp, double passengerTemp)
        {
            var commandResult = await teslaClient.SetTemps(vehicle.Id, driverTemp, passengerTemp);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> SeatHeaterRequest(TeslaVehicle vehicle, int heater, int level)
        {
            var commandResult = await teslaClient.SeatHeaterRequest(vehicle.Id, heater, level);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> SteeringWheelHeaterRequest(TeslaVehicle vehicle, bool on)
        {
            var commandResult = await teslaClient.SteeringWheelHeaterRequest(vehicle.Id, on);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> ChargePortOpen(TeslaVehicle vehicle)
        {
            var commandResult = await teslaClient.ChargePortOpen(vehicle.Id);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> ChargePortClose(TeslaVehicle vehicle)
        {
            var commandResult = await teslaClient.ChargePortClose(vehicle.Id);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> ChargeStart(TeslaVehicle vehicle)
        {
            var commandResult = await teslaClient.ChargeStart(vehicle.Id);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> ChargeStop(TeslaVehicle vehicle)
        {
            var commandResult = await teslaClient.ChargeStop(vehicle.Id);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> SetChargeLimit(TeslaVehicle vehicle, int percent)
        {
            var commandResult = await teslaClient.SetChargeLimit(vehicle.Id, percent);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> DoorUnlock(TeslaVehicle vehicle)
        {
            var commandResult = await teslaClient.DoorUnlock(vehicle.Id);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> DoorLock(TeslaVehicle vehicle)
        {
            var commandResult = await teslaClient.DoorLock(vehicle.Id);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> ActuateTrunk(TeslaVehicle vehicle, string whichTrunk)
        {
            var commandResult = await teslaClient.ActuateTrunk(vehicle.Id, whichTrunk);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> HonkHorn(TeslaVehicle vehicle)
        {
            var commandResult = await teslaClient.HonkHorn(vehicle.Id);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> FlashLights(TeslaVehicle vehicle)
        {
            var commandResult = await teslaClient.FlashLights(vehicle.Id);
            return CommandResult.From(commandResult.Data.Response);
        }
        public async Task<CommandResult> SunRoofControl(TeslaVehicle vehicle, string state)
        {
            var commandResult = await teslaClient.SunRoofControl(vehicle.Id, state);
            return CommandResult.From(commandResult.Data.Response);
        }

        private void VerifyAuthentication()
        {
            var hasAuth = httpClient.DefaultRequestHeaders.Authorization != null && teslaClient != null;
            if (!hasAuth)
            {
                throw new InvalidOperationException("Attempting network request that requries authentication without setting the bearer token.");
            }
        }
    }
}
