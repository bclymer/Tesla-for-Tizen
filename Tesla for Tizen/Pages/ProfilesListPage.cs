using System.Linq;
using TeslaTizen.Models;
using TeslaTizen.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class ProfilesListPage: CirclePage
    {
        public ProfilesListPage(TeslaVehicle vehicle) : this(vehicle, new ProfilesService()) { }

        public ProfilesListPage(TeslaVehicle vehicle, IProfileService profileService)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var profiles = profileService.GetProfiles();
            var listView = new CircleListView
            {
                Header = vehicle.Name,
                ItemsSource = profiles.Select(p => new ProfileBinder(p)),
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
                var binder = (ProfileBinder)e.Item;
                await Navigation.PushAsync(new ProfileActionPage(binder.Profile, profileService));
            };

            ActionButton = new ActionButtonItem
            {
                Text = "Create",
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new EditProfilePage());
                })
            };

            Content = new StackLayout
            {
                Children =
                {
                    listView,
                }
            };
        }

        private class ProfileBinder
        {
            public Profile Profile { get; }
            public string Name { get { return Profile.Name; } }

            public ProfileBinder(Profile profile)
            {
                Profile = profile;
            }
        }
    }
}
