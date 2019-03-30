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
    public class AddActionPage: CirclePage
    {
        public AddActionPage(Profile profile)
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var listView = new CircleListView
            {
                ItemsSource = VehicleActionUtils.GetVisibleActions().Select(a => a.GetDescription())
            };
            listView.ItemTapped += async (sender, e) => {
                //profile.Actions.Add();
                await Navigation.PopAsync();
            };

            Content = new StackLayout
            {
                Children =
                {
                    listView
                }
            };
        }
    }
}
