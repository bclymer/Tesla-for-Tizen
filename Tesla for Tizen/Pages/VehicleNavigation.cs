using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeslaTizen.Data;
using TeslaTizen.Models;
using TeslaTizen.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class VehicleNavigation: IndexPage
    {
        public TeslaService TeslaService { get; }
        private IProfileService ProfileService { get; }
        public ITeslaAPIWrapper TeslaAPIWrapper { get; }

        public VehicleNavigation(TeslaService teslaService, IProfileService profileService, ITeslaAPIWrapper teslaAPIWrapper)
        {
            TeslaService = teslaService;
            ProfileService = profileService;
            TeslaAPIWrapper = teslaAPIWrapper;
            NavigationPage.SetHasNavigationBar(this, false);
            Children.Add(new LoadingPage());
            var thread = new Thread(new ThreadStart(Setup))
            {
                IsBackground = true
            };
            thread.Start();
        }

        private async void Setup()
        {
            // TODO Need a way to determine if this is from cache or not to force a refresh
            var cachedVehicles = await TeslaService.GetVehicles();
            Device.BeginInvokeOnMainThread(() =>
            {
                ShowVehicles(cachedVehicles);
            });
            
        }

        private async void ShowVehicles(List<TeslaVehicle> vehicles)
        {
            var pages = vehicles.Select(v => new VehicleOverviewPage(v, ProfileService, TeslaAPIWrapper)).ToList();
            pages.ForEach(p => Children.Add(p));
            if (pages.Count == 1)
            {
                // If there is only 1 vehicle, go straight into it
                await pages.First().Navigation.PushAsync(new ProfilesListPage(vehicles.First(), ProfileService, TeslaAPIWrapper));
                // remove this page.
                Navigation.RemovePage(Navigation.NavigationStack[0]);
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
