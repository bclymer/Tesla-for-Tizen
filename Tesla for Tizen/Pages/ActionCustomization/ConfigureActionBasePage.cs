using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaTizen.Models;
using Tizen.Wearable.CircularUI.Forms;

namespace TeslaTizen.Pages
{
    public class ConfigureActionBasePage: CirclePage
    {
        private Profile Profile { get; }
        private VehicleActionType ActionType { get; }

        public ConfigureActionBasePage(Profile profile, VehicleActionType actionType)
        {
            Profile = profile;
            ActionType = actionType;
        }

        protected async Task Save(Dictionary<string, object> actionData)
        {
            Profile.Actions.Add(new VehicleAction
            {
                Type = ActionType,
                Params = actionData,
            });
            // Need to remove 2 pages at once, but can't double pop. So remove this manually now.
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Navigation.PopAsync();
        }
    }
}
