using TeslaTizen.Models;
using TeslaTizen.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class VehicleOverviewPage : CirclePage
    {
        public IProfileService ProfileService { get; }

        public VehicleOverviewPage(TeslaVehicle vehicle, IProfileService profileService)
        {
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        FontSize = 20,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = vehicle.Name,
                    },
                },
                GestureRecognizers =
                {
                    new TapGestureRecognizer
                    {
                        Command = new Command(async () =>
                        {
                            await Navigation.PushAsync(new ProfilesListPage(vehicle, ProfileService));
                        }),
                    }
                }
            };
            ProfileService = profileService;
        }
    }
}
