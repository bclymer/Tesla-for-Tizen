using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TeslaTizen.Models
{
    public class Profile
    {
        public string Name { get; set; } = "New Profile";
        public ObservableCollection<VehicleAction> Actions { get; set; } = new ObservableCollection<VehicleAction>
        {
            new VehicleAction
            {
                Type = VehicleActionType.WakeUp,
            }
        };
    }
}
