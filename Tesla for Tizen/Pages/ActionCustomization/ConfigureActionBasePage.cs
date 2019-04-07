using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaTizen.Models;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public abstract class ConfigureActionBasePage: CirclePage
    {
        private Profile Profile { get; }
        private VehicleActionType ActionType { get; }
        // Exists when the action is being edited. 
        protected VehicleAction Action { get; }

        public ConfigureActionBasePage(Profile profile, VehicleActionType actionType, VehicleAction action)
        {
            Profile = profile;
            ActionType = actionType;
            Action = action;
            Content = SetupView();
        }

        public abstract View SetupView();

        protected async Task Save(Dictionary<string, object> actionData)
        {
            if (Action != null)
            {
                Action.Params = actionData;
                await Navigation.PopAsync();
            }
            else
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
}
