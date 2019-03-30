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
            
            var listView = new CircleListView
            {
                ItemsSource = profile.Actions,
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
            listView.ItemTapped += async (sender, e) =>
            {
                var binder = (VehicleAction)e.Item;
                await DisplayAlert("Tapped", $"You tapped {binder.Name}", "Cancel");
            };
            // TODO tapping cell should have popup to delete it.

            Content = new StackLayout
            {
                Children =
                {
                    listView,
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
