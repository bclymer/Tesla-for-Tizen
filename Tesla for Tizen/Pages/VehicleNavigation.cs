using System.Collections.Generic;
using System.Linq;
using Tesla_for_Tizen;
using TeslaTizen.Models;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class VehicleNavigation: TabbedPage
    {
        public VehicleNavigation(TeslaService teslaService)
        {
            // Need a way to determine if this is from cache or not to force a refresh
            var cachedVehicles = teslaService.GetVehicles();
            ShowVehicles(cachedVehicles);
        }

        private void ShowLoading()
        {
            Children.Clear();
            Children.Add(new LoadingPage());
        }

        private void ShowVehicles(List<TeslaVehicle> vehicles)
        {
            Children.Clear();
            var pages = vehicles.Select(v => new VehicleOverviewPage(v)).ToList();
            pages.ForEach(p => Children.Add(p));
            if (pages.Count == 1)
            {
                // If there is only 1 vehicle, go straight into it
                var profiles = new ProfilesListPage(vehicles.First());
                NavigationPage.SetHasNavigationBar(profiles, false);
                pages.First().Navigation.PushAsync(profiles);
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
