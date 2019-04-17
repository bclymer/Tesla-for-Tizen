using System;
using System.Threading;
using System.Threading.Tasks;
using TeslaTizen.Data;
using TeslaTizen.Services;
using TeslaTizen.Utils;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class LoginPage: CirclePage
    {
        private readonly TeslaService TeslaService;
        public readonly IProfileService ProfileService;
        public readonly ITeslaAPIWrapper TeslaAPIWrapper;

        private readonly Entry Email;
        private readonly Entry Password;
        private readonly Button LoginButton;

        public LoginPage(TeslaService teslaService, IProfileService profileService, ITeslaAPIWrapper teslaAPIWrapper)
        {
            TeslaService = teslaService;
            ProfileService = profileService;
            TeslaAPIWrapper = teslaAPIWrapper;

            Email = new Entry
            {
                Placeholder = "Email",
                Keyboard = Keyboard.Numeric,
            };
            Email.TextChanged += TextChanged;
            Password = new Entry
            {
                Placeholder = "Password",
                Keyboard = Keyboard.Numeric,
            };
            Password.TextChanged += TextChanged;
            LoginButton = new Button
            {
                Text = "Login",
                IsEnabled = false,
            };
            LoginButton.Clicked += Button_Clicked;

            Content = new StackLayout
            {
                Children =
                {
                    Email,
                    Password,
                    LoginButton,
                },
            };
        }

        // TODO need to actually login and store the result.
        private void Button_Clicked(object sender, EventArgs e)
        {
            // TODO loading UI
            var thread = new Thread(new ThreadStart(Authenticate))
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            LoginButton.IsEnabled = (Email.Text.Length > 3 && Email.Text.Contains("@")) &&
                Password.Text.Length > 0;
        }

        private async void Authenticate()
        {
            try
            {
                await TeslaService.Login(Email.Text, Password.Text);
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage = new NavigationPage(new VehicleNavigation(TeslaService, ProfileService, TeslaAPIWrapper));
                });
            }
            catch
            {
                // TODO handle error
            }
        }
    }
}
