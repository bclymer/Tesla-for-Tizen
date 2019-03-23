using System.Linq;
using TeslaTizen.Models;
using TeslaTizen.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class ProfilesListPage: CirclePage
    {
        public ProfilesListPage(TeslaVehicle vehicle) : this(vehicle, new ProfilesService()) { }

        public ProfilesListPage(TeslaVehicle vehicle, IProfileService profileService)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var profiles = profileService.GetProfiles();
            var listView = new CircleListView
            {
                ItemsSource = profiles.Select(p => p.Name),
            };
            listView.ItemTapped += async (sender, e) => {
                await DisplayAlert("Tapped", e.Item + " row was tapped", "OK");
                ((ListView)sender).SelectedItem = null; // de-select the row
            };

            ActionButton = new ActionButtonItem
            {
                Text = "Create",
                Command = new Command(async () =>
                {
                    await DisplayAlert("Tapped", "Extra Item", "OK");
                })
            };

            Content = new StackLayout
            {
                Children =
                {
                    listView,
                }
            };
        }
    }
}
