using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaTizen.Models;
using TeslaTizen.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class AddActionPage: CirclePage
    {
        public AddActionPage(Profile profile, IProfileService profileService)
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var listView = new CircleListView
            {
                ItemsSource = VehicleActionUtils.GetUserActions().Select(a => new VehicleActionBinder(a)),
                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label
                    {
                        HeightRequest = 120,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                    };
                    nameLabel.SetBinding(Label.TextProperty, "Name");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Children =
                            {
                                nameLabel,
                            }
                        }
                    };
                }),
            };
            listView.ItemTapped += async (sender, e) => {
                var binder = (VehicleActionBinder)e.Item;
                await binder.Action.CustomizeOrReturn(profile, null, Navigation, profileService);
            };

            Content = listView;
        }

        private class VehicleActionBinder
        {
            public VehicleActionType Action { get; }
            public string Name { get { return Action.GetDescription(); } }

            public VehicleActionBinder(VehicleActionType action)
            {
                Action = action;
            }
        }
    }
}
