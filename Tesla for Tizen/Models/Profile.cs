using System.Collections.Generic;

namespace TeslaTizen.Models
{
    public class Profile
    {
        public string Name { get; set; } = "New Profile";
        public List<VehicleAction> Actions { get; set; } = new List<VehicleAction>
        {
            new VehicleAction
            {
                Type = VehicleActionType.WakeUp,
            }
        };
    }
}
