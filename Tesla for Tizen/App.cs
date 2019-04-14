using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using TeslaTizen.Data;
using TeslaTizen.Pages;
using TeslaTizen.Services;
using TeslaTizen.Utils;
using Xamarin.Forms;

// TODO - Refresh token every ~30 days.
// TODO - Create widget for quick launch of profiles.
// TODO - Redesign the car list screen to show your car's details
// TODO - Handle errors basically everywhere there is an API request.

namespace TeslaTizen
{
    public class App : Application
    {
        private readonly IProfileService profileService = new ProfileService();
        private readonly ITeslaAPIWrapper teslaAPIWrapper = new TeslaAPIWrapper();
        private TeslaService TeslaService => new TeslaService(new TeslaCache(), teslaAPIWrapper);

        public App()
        {
            CacheFactory.CreateCache().Init();
            LogUtil.Info($"IsFreshInstall={CacheFactory.CreateCache().IsFreshInstall()}");
            var service = TeslaService;
            if (service.RequiresLogin())
            {
                MainPage = new LoginPage(service, profileService, teslaAPIWrapper);
            }
            else
            {
                MainPage = new NavigationPage(new VehicleNavigation(service, profileService, teslaAPIWrapper));
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
