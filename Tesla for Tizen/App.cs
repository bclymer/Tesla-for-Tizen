using TeslaTizen.Pages;
using Xamarin.Forms;

namespace Tesla_for_Tizen
{
    public class App : Application
    {
        private readonly TeslaService teslaService = new TeslaService();

        public App()
        {
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
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
