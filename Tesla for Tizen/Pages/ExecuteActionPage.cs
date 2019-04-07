using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaTizen.Data;
using TeslaTizen.Models;
using Tizen.Wearable.CircularUI.Forms;

namespace TeslaTizen.Pages
{
    public class ExecuteActionPage: CirclePage
    {
        public ExecuteActionPage(Profile profile, TeslaVehicle vehicle) : this(profile, vehicle, new TeslaAPIWrapper()) { }

        public ExecuteActionPage(Profile profile, TeslaVehicle vehicle, ITeslaAPIWrapper teslaAPIWrapper)
        {
            ExecuteAction(profile, vehicle, teslaAPIWrapper).Wait();
        }

        public async Task ExecuteAction(Profile profile, TeslaVehicle vehicle, ITeslaAPIWrapper teslaAPIWrapper)
        {
            foreach (VehicleAction action in profile.Actions)
            {
                await action.Execute(vehicle, teslaAPIWrapper);
            }
        }
    }
}
