using System.Diagnostics;
using TeslaTizen.Data;
using TeslaTizen.Pages;
using TeslaTizen.Services;
using TeslaTizen.Utils;
using Xamarin.Forms;

namespace TeslaTizen
{
    public class App : Application
    {
        private readonly TeslaService teslaService = new TeslaService();

        public App()
        {
            CacheFactory.CreateCache().Init();
            LogUtil.Info($"IsFreshInstall={CacheFactory.CreateCache().IsFreshInstall()}");
            if (teslaService.RequiresLogin())
            {
                MainPage = new LoginPage(teslaService);
            }
            else
            {
                MainPage = new NavigationPage(new VehicleNavigation(teslaService));
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
