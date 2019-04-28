using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaTizen.Data;
using TeslaTizen.Models;
using TeslaTizen.Services;
using TeslaTizen.Utils;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class ProfileActionPage: CirclePage
    {
        public ProfileActionPage(Profile profile, TeslaVehicle teslaVehicle, IProfileService profileService, ITeslaAPIWrapper teslaAPIWrapper)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var listView = new CircleListView
            {
                Header = UIUtil.CreateHeaderLabel(profile.Name),
                ItemsSource = new List<string> { "Run", "Edit", "Delete" },
            };
            listView.ItemTapped += async (sender, e) =>
            {
                switch (e.Item)
                {
                    case "Run":
                        await Navigation.PushAsync(new ExecuteActionPage(profile, teslaVehicle, teslaAPIWrapper));
                        break;
                    case "Edit":
                        await Navigation.PushAsync(new EditProfilePage(profile, profileService));
                        // Remove this page. After editing the action this list should be gone.
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                        break;
                    case "Delete":
                        await profileService.DeleteProfileAsync(profile);
                        await Navigation.PopAsync();
                        break;
                    default:
                        break;
                }
            };
            Content = listView;
        }
    }
}
