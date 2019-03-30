using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaTizen.Models;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class EditProfilePage: CirclePage
    {
        public EditProfilePage() : this(new Profile()) { }

        public EditProfilePage(Profile profile)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Content = new StackLayout
            {
                Children =
                {
                    new CircleListView
                    {
                        ItemsSource = profile.Actions.Select(a => a.Type.GetDescription()),
                    }
                }
            };

            ActionButton = new ActionButtonItem
            {
                Text = "+",
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new AddActionPage(profile));
                })
            };
        }
    }
}
