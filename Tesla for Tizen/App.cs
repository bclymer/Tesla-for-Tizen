using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using TeslaTizen.Data;
using TeslaTizen.Pages;
using TeslaTizen.Services;
using TeslaTizen.Utils;
using Xamarin.Forms;

namespace TeslaTizen
{
    public class App : Application
    {
        // TODO Pretty sure this can just be a new instance.
        private readonly IProfileService profileService = ProfileService.Instance;
        private readonly ITeslaAPIWrapper teslaAPIWrapper = new TeslaAPIWrapper();
        private TeslaService TeslaService => new TeslaService(new TeslaCache(), teslaAPIWrapper);

        public App()
        {
            CacheFactory.CreateCache().Init();
            LogUtil.Info($"IsFreshInstall={CacheFactory.CreateCache().IsFreshInstall()}");
            var service = TeslaService;
            if (service.RequiresLogin())
            {
                MainPage = new LoginPage(service);
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
