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
        private Entry DriverTemp { get; }
        private Entry PassengerTemp { get; }
        private Button SaveButton { get; }

        public ConfigureClimateSetTemps(Profile profile, VehicleActionType action) : base(profile, action)
        {
            DriverTemp = new Entry
            {
                Placeholder = "Driver Temp in C",
                Keyboard = Keyboard.Numeric,
            };
            DriverTemp.TextChanged += TextChanged;
            PassengerTemp = new Entry
            {
                Placeholder = "Passenger Temp in C",
                Keyboard = Keyboard.Numeric,
            };
            PassengerTemp.TextChanged += TextChanged;
            SaveButton = new Button
            {
                Text = "Save",
                IsEnabled = false,
            };
            SaveButton.Clicked += SaveButton_Clicked;
            Content = new StackLayout
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
