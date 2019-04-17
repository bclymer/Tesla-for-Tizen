using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeslaTizen.Models;
using TeslaTizen.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class RenameProfilePage : CirclePage
    {
        public RenameProfilePage(Profile profile, IProfileService profileService)
        {
            var nameEntry = new Entry
            {
                Text = profile.Name
            };

            var saveButton = new Button
            {
                Text = "Save",
                Command = new Command(async () =>
                {
                    profile.Name = nameEntry.Text;
                    await profileService.UpsertProfileAsync(profile);
                    await Navigation.PopModalAsync();
                })
            };

            nameEntry.TextChanged += (sender, e) =>
            {
                saveButton.IsEnabled = nameEntry.Text.Length > 0;
            };

            Content = new StackLayout
            {
                Children = 
                {
                    nameEntry,
                    saveButton
                }
            };
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
