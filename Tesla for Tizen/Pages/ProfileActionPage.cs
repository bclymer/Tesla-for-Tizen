using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaTizen.Models;
using TeslaTizen.Services;
using Tizen.Wearable.CircularUI.Forms;

namespace TeslaTizen.Pages
{
    public class ProfileActionPage: CirclePage
    {
        public ProfileActionPage(Profile profile, IProfileService profileService)
        {
            var listView = new CircleListView
            {
                Header = profile.Name,
                ItemsSource = new List<string> { "Run", "Edit", "Delete" },
            };
            listView.ItemTapped += async (sender, e) =>
            {
                switch (e.Item)
                {
                    case "Run":
                        // push execute action page which shows progress.
                        break;
                    case "Edit":
                        await Navigation.PushAsync(new EditProfilePage(profile));
                        break;
                    case "Delete":
                        // Confirm delete
                        //profileService.DeleteProfile(profile);
                        break;
                    default:
                        break;
                }
            };
            Content = listView;
        }
    }
}
