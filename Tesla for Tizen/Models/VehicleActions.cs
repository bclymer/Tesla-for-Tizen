using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tesla_for_Tizen;
using TeslaTizen.Pages;
using TeslaTizen.Pages.ActionCustomization;
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
        WakeUp = -1,
        ClimateACStart = 0,
        ClimateACStop = 1,
        ClimateSetTemps = 2,
        ClimateHeatedSeat = 3,
        ClimateHeatedSteeringWheel = 4,
        ChargingStart = 5,
        ChargingStop = 6,
        ChargingSetLimit = 7,
        ChargingOpenPort = 8,
        ChargingClosePort = 9,
        ControlsDoorsLock = 10,
        ControlsDoorsUnlock = 11,
        ControlsFlashLights = 12,
        ControlsHonkHorn = 13,
        ControlsFrontTrunk = 14,
        ControlsRearTrunk = 15,
        ControlsSunRoof = 16,
        RemoteStartDrive = 17,
        SpeedLimitActivate = 18,
        SpeedLimitClearPin = 19,
        SpeedLimitDeactivate = 20,
        SpeedLimitSet = 21,
        ValetModeResetPin = 22,
        ValetModeSet = 23,
    }

    public static class VehicleActionUtils
    {
        public static IEnumerable<VehicleActionType> GetVisibleActions()
        {
            return Enum.GetValues(typeof(VehicleActionType)).Cast<VehicleActionType>().Where(a => a != VehicleActionType.WakeUp);
        }

        public static bool IsVisible(this VehicleActionType action)
        {
            return action != VehicleActionType.WakeUp;
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

        public static async Task CustomizeOrReturn(this VehicleActionType action, Profile profile, INavigation navigation)
        {
            switch (action)
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
                        Type = action,
                    });
                    await navigation.PopAsync();
                    break;
                case VehicleActionType.ClimateSetTemps:
                    await navigation.PushAsync(new ConfigureClimateSetTemps(profile, action));
                    break;
                case VehicleActionType.ClimateHeatedSeat:
                case VehicleActionType.ClimateHeatedSteeringWheel:
                case VehicleActionType.ChargingSetLimit:
                case VehicleActionType.ControlsRearTrunk:
                case VehicleActionType.ControlsSunRoof:
                case VehicleActionType.SpeedLimitSet:
                    await navigation.PushAsync(new ConfigureActionBasePage(profile, action));
                    break;
            }
        }

        public static Task Execute(this VehicleAction action, TeslaAPIWrapper teslaApi)
        {
            // TODO all of this.
            switch (action.Type)
            {
                case VehicleActionType.ClimateACStart:
                    
                case VehicleActionType.ClimateACStop:

                case VehicleActionType.ClimateSetTemps:

                case VehicleActionType.ClimateHeatedSeat:

                case VehicleActionType.ClimateHeatedSteeringWheel:

                case VehicleActionType.ChargingStart:

                case VehicleActionType.ChargingStop:

                case VehicleActionType.ChargingSetLimit:

                case VehicleActionType.ChargingOpenPort:

                case VehicleActionType.ChargingClosePort:

                case VehicleActionType.ControlsDoorsLock:

                case VehicleActionType.ControlsDoorsUnlock:

                case VehicleActionType.ControlsFlashLights:

                case VehicleActionType.ControlsHonkHorn:

                case VehicleActionType.ControlsFrontTrunk:

                case VehicleActionType.ControlsRearTrunk:

                case VehicleActionType.ControlsSunRoof:

                case VehicleActionType.RemoteStartDrive:

                case VehicleActionType.SpeedLimitActivate:

                case VehicleActionType.SpeedLimitClearPin:

                case VehicleActionType.SpeedLimitDeactivate:

                case VehicleActionType.SpeedLimitSet:

                case VehicleActionType.ValetModeResetPin:

                case VehicleActionType.ValetModeSet:
                    break;
            }
            return Task.CompletedTask;
        }
    }
}
