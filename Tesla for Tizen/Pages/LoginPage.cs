using System;
using System.Threading;
using TeslaTizen.Services;
using TeslaTizen.Utils;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class LoginPage: CirclePage
    {
        private readonly TeslaService teslaService;

        public LoginPage(TeslaService teslaService)
        {
            this.teslaService = teslaService;
            // TODO need to set up UI for text entry.
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var thread = new Thread(new ThreadStart(Authenticate))
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void Authenticate()
        {
            var vehicles = teslaService.GetVehicles();
            Device.BeginInvokeOnMainThread(() =>
            {
                // TODO let main page know login is ready.
            });
        }

        private void Login()
        {
            var developer = LocalFileParser.GetDeveloper();
            teslaService.Login(developer.Email, developer.Password);
        }
    }
}
