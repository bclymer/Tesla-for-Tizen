using TeslaTizen.Models;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class VehicleOverviewPage : CirclePage
    {
        public VehicleOverviewPage(TeslaVehicle vehicle)
        {
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
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
                            await Navigation.PushAsync(new ProfilesListPage(vehicle));
                        }),
                    }
                }
            };
        }
    }
}
