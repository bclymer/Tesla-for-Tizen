using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TeslaTizen.Models
{
    public class Profile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<VehicleAction> Actions { get; set; }

        public static Profile Create()
        {
            return new Profile
            {
                Id = Guid.NewGuid().ToString(),
                Name = "New Profile",
                Actions = new List<VehicleAction>
                {
                    new VehicleAction
                    {
                        Type = VehicleActionType.WakeUp,
                    }
                },
            };
        }
    }
}
