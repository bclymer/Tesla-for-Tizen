using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaTizen.Models;
using Xamarin.Forms;

namespace TeslaTizen.Pages.ActionCustomization
{
    public class ConfigureClimateSetTemps : ConfigureActionBasePage
    {
        public ConfigureClimateSetTemps(Profile profile, VehicleActionType actionType, VehicleAction action) : base(profile, actionType, action) { }

        private Entry DriverTemp { get; set; }
        private Entry PassengerTemp { get; set; }
        private Button SaveButton { get; set; }

        public override View SetupView()
        {
            var existingDriverTemp = Action?.Params?.ContainsKey("driver_temp") == true ? ((double)Action.Params["driver_temp"]).ToString() : null;
            var existingPassengerTemp = Action?.Params?.ContainsKey("passenger_temp") == true ? ((double)Action.Params["passenger_temp"]).ToString() : null;
            DriverTemp = new Entry
            {
                Placeholder = "Driver Temp in C",
                Text = existingDriverTemp,
                Keyboard = Keyboard.Numeric,
            };
            DriverTemp.TextChanged += TextChanged;
            PassengerTemp = new Entry
            {
                Placeholder = "Passenger Temp in C",
                Text = existingPassengerTemp,
                Keyboard = Keyboard.Numeric,
            };
            PassengerTemp.TextChanged += TextChanged;
            SaveButton = new Button
            {
                Text = "Save",
                IsEnabled = false,
            };
            SaveButton.Clicked += SaveButton_Clicked;

            // If there are default values, check if they're valid.
            TextChanged(null, null);

            return new StackLayout
            {
                Children =
                {
                    DriverTemp,
                    PassengerTemp,
                    SaveButton,
                }
            };
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButton.IsEnabled = double.TryParse(DriverTemp.Text, out double driver) && double.TryParse(PassengerTemp.Text, out double passenger);
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            await Save(new Dictionary<string, object>
            {
                { "driver_temp", double.Parse(DriverTemp.Text) },
                { "passenger_temp", double.Parse(PassengerTemp.Text) },
            });
        }
    }
}
