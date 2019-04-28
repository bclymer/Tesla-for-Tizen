using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeslaTizen.Data;
using TeslaTizen.Pages;
using TeslaTizen.Pages.ActionCustomization;
using TeslaTizen.Services;
using TeslaTizen.Utils;
using Xamarin.Forms;

namespace TeslaTizen.Models
{
    public class VehicleAction
    {
        public string Name { get { return Type.GetDescription(); } }
        public VehicleActionType Type { get; set; }
        public Dictionary<string, object> Params { get; set; }
    }

    public enum VehicleActionType: int
    {
        WakeUp = 1,
        ClimateACStart = 2,
        ClimateACStop = 3,
        ClimateSetTemps = 4,
        ClimateHeatedSeat = 5,
        ClimateHeatedSteeringWheel = 6,
        ChargingStart = 7,
        ChargingStop = 8,
        ChargingSetLimit = 9,
        ChargingOpenPort = 10,
        ChargingClosePort = 11,
        ControlsDoorsLock = 12,
        ControlsDoorsUnlock = 13,
        ControlsFlashLights = 14,
        ControlsHonkHorn = 15,
        ControlsFrontTrunk = 16,
        ControlsRearTrunk = 17,
        ControlsSunRoof = 18,
        RemoteStartDrive = 19,
        SpeedLimitActivate = 20,
        SpeedLimitClearPin = 21,
        SpeedLimitDeactivate = 22,
        SpeedLimitSet = 23,
        ValetModeResetPin = 24,
        ValetModeSet = 25,
    }

    public static class VehicleActionUtils
    {
        public static IEnumerable<VehicleActionType> GetUserActions()
        {
            return Enum.GetValues(typeof(VehicleActionType)).Cast<VehicleActionType>().Where(a => !a.IsRequired());
        }

        public static bool IsRequired(this VehicleActionType action)
        {
            return action == VehicleActionType.WakeUp;
        }

        public static bool IsCustomizable(this VehicleActionType action)
        {
            switch (action)
            {
                case VehicleActionType.ClimateSetTemps:
                case VehicleActionType.ClimateHeatedSeat:
                case VehicleActionType.ClimateHeatedSteeringWheel:
                case VehicleActionType.ChargingSetLimit:
                case VehicleActionType.ControlsRearTrunk:
                case VehicleActionType.ControlsSunRoof:
                case VehicleActionType.SpeedLimitSet:
                    return true;
                default:
                    return false;
            }
        }

        public static string GetDescription(this VehicleActionType action)
        {
            switch (action)
            {
                case VehicleActionType.WakeUp: return "Wake Up";
                case VehicleActionType.ClimateACStart: return "Climate - Start AC";
                case VehicleActionType.ClimateACStop: return "Climate - Stop AC";
                case VehicleActionType.ClimateSetTemps: return "Climate - Set Temps";
                case VehicleActionType.ClimateHeatedSeat: return "Climate - Heated Sets";
                case VehicleActionType.ClimateHeatedSteeringWheel: return "Climate - Heated Steering Wheel";
                case VehicleActionType.ChargingStart: return "Charging - Start";
                case VehicleActionType.ChargingStop: return "Charging - Stop";
                case VehicleActionType.ChargingSetLimit: return "Charging - Set Limit";
                case VehicleActionType.ChargingOpenPort: return "Charging - Open Port";
                case VehicleActionType.ChargingClosePort: return "Charging - Close Port";
                case VehicleActionType.ControlsDoorsLock: return "Controls - Lock Doors";
                case VehicleActionType.ControlsDoorsUnlock: return "Controls - Unlock Doors";
                case VehicleActionType.ControlsFlashLights: return "Controls - Flash Lights";
                case VehicleActionType.ControlsHonkHorn: return "Controls - Honk Horn";
                case VehicleActionType.ControlsFrontTrunk: return "Controls - Open Front Trunk";
                case VehicleActionType.ControlsRearTrunk: return "Controls - Toggle Rear Trunk";
                case VehicleActionType.ControlsSunRoof: return "Controls - Sun Roof";
                case VehicleActionType.RemoteStartDrive: return "Controls - Remote Start Drive";
                case VehicleActionType.SpeedLimitActivate: return "Speed Limit - Activate";
                case VehicleActionType.SpeedLimitClearPin: return "Speed Limit - Clear PIN";
                case VehicleActionType.SpeedLimitDeactivate: return "Speed Limit - Deactivate";
                case VehicleActionType.SpeedLimitSet: return "Speed Limit - Set Limit";
                case VehicleActionType.ValetModeResetPin: return "Valet Mode - Reset PIN";
                case VehicleActionType.ValetModeSet: return "Valet Mode - Set";
            }
            return "Unknown";
        }

        public static async Task CustomizeOrReturn(this VehicleActionType actionType, Profile profile, VehicleAction action, INavigation navigation, IProfileService profileService)
        {
            switch (actionType)
            {
                case VehicleActionType.ClimateACStart:
                case VehicleActionType.ClimateACStop:
                case VehicleActionType.ChargingStart:
                case VehicleActionType.ChargingStop:
                case VehicleActionType.ChargingOpenPort:
                case VehicleActionType.ChargingClosePort:
                case VehicleActionType.ControlsDoorsLock:
                case VehicleActionType.ControlsDoorsUnlock:
                case VehicleActionType.ControlsFlashLights:
                case VehicleActionType.ControlsHonkHorn:
                case VehicleActionType.ControlsFrontTrunk:
                case VehicleActionType.RemoteStartDrive:
                case VehicleActionType.SpeedLimitActivate:
                case VehicleActionType.SpeedLimitClearPin:
                case VehicleActionType.SpeedLimitDeactivate:
                case VehicleActionType.ValetModeResetPin:
                case VehicleActionType.ValetModeSet:
                    profile.Actions.Add(new VehicleAction
                    {
                        Type = actionType,
                    });
                    await profileService.UpsertProfileAsync(profile);
                    await navigation.PopAsync();
                    break;
                case VehicleActionType.ClimateSetTemps:
                    await navigation.PushAsync(new ConfigureClimateSetTemps(profile, actionType, action, profileService));
                    break;
                case VehicleActionType.ClimateHeatedSeat:
                case VehicleActionType.ClimateHeatedSteeringWheel:
                case VehicleActionType.ChargingSetLimit:
                case VehicleActionType.ControlsRearTrunk:
                case VehicleActionType.ControlsSunRoof:
                case VehicleActionType.SpeedLimitSet:
                    //await navigation.PushAsync(new ConfigureActionBasePage(profile, actionType, action));
                    break;
            }
        }

        public static async Task Execute(this VehicleAction action, TeslaVehicle vehicle, ITeslaAPIWrapper teslaApi)
        {
            LogUtil.Debug($"Start Executing {action.Type.GetDescription()}");
            // TODO remaining actions
            switch (action.Type)
            {
                case VehicleActionType.WakeUp:
                    await teslaApi.WakeUp(vehicle);
                    break;
                case VehicleActionType.ClimateACStart:
                    await teslaApi.StartAutoConditioning(vehicle);
                    break;
                case VehicleActionType.ClimateACStop:
                    await teslaApi.StopAutoConditioning(vehicle);
                    break;
                case VehicleActionType.ClimateSetTemps:
                    var driverTemp = (double)action.Params["driver_temp"];
                    var passengerTemp = (double)action.Params["passenger_temp"];
                    await teslaApi.SetTemps(vehicle, driverTemp, passengerTemp);
                    break;
                case VehicleActionType.ClimateHeatedSeat:
                    var heater = (int)action.Params["heater"];
                    var level = (int)action.Params["level"];
                    await teslaApi.SeatHeaterRequest(vehicle, heater, level);
                    break;
                case VehicleActionType.ClimateHeatedSteeringWheel:
                    var on = (bool)action.Params["on"];
                    await teslaApi.SteeringWheelHeaterRequest(vehicle, on);
                    break;
                case VehicleActionType.ChargingStart:
                    await teslaApi.ChargeStart(vehicle);
                    break;
                case VehicleActionType.ChargingStop:
                    await teslaApi.ChargeStop(vehicle);
                    break;
                case VehicleActionType.ChargingSetLimit:
                    var percent = (int)action.Params["percent"];
                    await teslaApi.SetChargeLimit(vehicle, percent);
                    break;
                case VehicleActionType.ChargingOpenPort:
                    await teslaApi.ChargePortOpen(vehicle);
                    break;
                case VehicleActionType.ChargingClosePort:
                    await teslaApi.ChargePortClose(vehicle);
                    break;
                case VehicleActionType.ControlsDoorsLock:
                    await teslaApi.DoorLock(vehicle);
                    break;
                case VehicleActionType.ControlsDoorsUnlock:
                    await teslaApi.DoorUnlock(vehicle);
                    break;
                case VehicleActionType.ControlsFlashLights:
                    await teslaApi.FlashLights(vehicle);
                    break;
                case VehicleActionType.ControlsHonkHorn:
                    await teslaApi.HonkHorn(vehicle);
                    break;
                case VehicleActionType.ControlsFrontTrunk:
                    await teslaApi.ActuateTrunk(vehicle, "front");
                    break;
                case VehicleActionType.ControlsRearTrunk:
                    await teslaApi.ActuateTrunk(vehicle, "rear");
                    break;
                case VehicleActionType.ControlsSunRoof:
                    var state = (string)action.Params["state"];
                    await teslaApi.SunRoofControl(vehicle, state);
                    break;
                case VehicleActionType.RemoteStartDrive:

                case VehicleActionType.SpeedLimitActivate:

                case VehicleActionType.SpeedLimitClearPin:

                case VehicleActionType.SpeedLimitDeactivate:

                case VehicleActionType.SpeedLimitSet:

                case VehicleActionType.ValetModeResetPin:

                case VehicleActionType.ValetModeSet:
                    Thread.Sleep(500);
                    break;
            }
            LogUtil.Debug($"Finished Executing {action.Type.GetDescription()}");
        }
    }
}
