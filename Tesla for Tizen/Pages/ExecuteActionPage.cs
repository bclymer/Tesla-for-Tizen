using System.Threading;
using System.Threading.Tasks;
using TeslaTizen.Data;
using TeslaTizen.Models;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class ExecuteActionPage: CirclePage
    {
        public Label CurrentActionLabel { get; set; }
        public Profile Profile { get; }
        public TeslaVehicle Vehicle { get; }
        public ITeslaAPIWrapper TeslaAPIWrapper { get; }

        public ExecuteActionPage(Profile profile, TeslaVehicle vehicle, ITeslaAPIWrapper teslaAPIWrapper)
        {
            CurrentActionLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            Content = CurrentActionLabel;
            Profile = profile;
            Vehicle = vehicle;
            TeslaAPIWrapper = teslaAPIWrapper;
        }

        public async void ExecuteAction()
        {
            foreach (VehicleAction action in Profile.Actions)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    CurrentActionLabel.Text = $"Executing: {action.Name}";
                });
                await action.Execute(Vehicle, TeslaAPIWrapper);
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var thread = new Thread(new ThreadStart(ExecuteAction))
            {
                IsBackground = true
            };
            thread.Start();
        }
    }
}
