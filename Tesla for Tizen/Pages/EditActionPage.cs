using ElmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeslaTizen.Models;
using TeslaTizen.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Label = Xamarin.Forms.Label;

namespace TeslaTizen.Pages
{
    public class EditActionPage : TwoButtonPage
    {
        public EditActionPage(Profile profile, VehicleAction action, IProfileService profileService)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            FirstButton = new MenuItem
            {
                Icon = new FileImageSource
                {
                    File = "b_option_list_icon_delete.png",
                }
            };
            FirstButton.Command = new Command(async () =>
            {
                profile.Actions.Remove(action);
                await profileService.UpsertProfileAsync(profile);
                await Navigation.PopAsync();
            });
            if (action.Type.IsCustomizable())
            {
                SecondButton = new MenuItem
                {
                    Icon = new FileImageSource
                    {
                        File = "baseline_edit_white_36dp.png",
                    }
                };
                SecondButton.Command = new Command(async () =>
                {
                    await action.Type.CustomizeOrReturn(profile, action, Navigation, profileService);
                });
            }
            Content = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Text = action.Name,
            };
        }
    }
}
