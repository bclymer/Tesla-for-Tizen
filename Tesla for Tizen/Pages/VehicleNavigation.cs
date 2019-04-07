using System.Collections.Generic;
using System.Linq;
using TeslaTizen.Models;
using TeslaTizen.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class VehicleNavigation: IndexPage
    {
        private IProfileService ProfileService { get; }

        public VehicleNavigation(TeslaService teslaService, IProfileService profileService)
        {
            ProfileService = profileService;
            NavigationPage.SetHasNavigationBar(this, false);
            // Need a way to determine if this is from cache or not to force a refresh
            var cachedVehicles = teslaService.GetVehicles();
            ShowVehicles(cachedVehicles);
        }

        private void ShowLoading()
        {
            Children.Clear();
            Children.Add(new LoadingPage());
        }

        private async void ShowVehicles(List<TeslaVehicle> vehicles)
        {
            Children.Clear();
            var pages = vehicles.Select(v => new VehicleOverviewPage(v, ProfileService)).ToList();
            pages.ForEach(p => Children.Add(p));
            if (pages.Count == 1)
            {
                // If there is only 1 vehicle, go straight into it
                await pages.First().Navigation.PushAsync(new ProfilesListPage(vehicles.First(), ProfileService));
            }
        }

        private class LoadingPage: CirclePage
        {
            public LoadingPage()
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label
                        {
                            Text = "Loading..."
                        }
                    }
                };
            }
        }
    }
}
