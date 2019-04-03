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
    public class EditProfilePage: CirclePage
    {
        private readonly IProfileService ProfileService;
        private readonly Profile Profile;

        public EditProfilePage() : this(Profile.Create(), new ProfilesService()) { }

        public EditProfilePage(Profile profile) : this(profile, new ProfilesService()) { }

        public EditProfilePage(Profile profile, IProfileService profileService)
        {
            Profile = profile;
            ProfileService = profileService;

            NavigationPage.SetHasNavigationBar(this, false);
            
            var listView = new CircleListView
            {
                Header = profile.Name,
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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ProfileService.UpsertProfileAsync(Profile);
        }
    }
}
