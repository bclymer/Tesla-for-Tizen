using System.Collections.Generic;
using System;
using System.Linq;
using TeslaTizen.Models;
using TeslaTizen.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using TeslaTizen.Utils;
using System.Threading.Tasks;

namespace TeslaTizen.Pages
{
    public class ProfilesListPage: CirclePage
    {
        private IDisposable Disposable;

        public ProfilesListPage(TeslaVehicle vehicle, IProfileService profileService)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var listView = new CircleListView
            {
                Header = UIUtil.CreateHeaderLabel(vehicle.Name),
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
                await Navigation.PushAsync(new ProfileActionPage(binder.Profile, vehicle, profileService));
            };

            ActionButton = new ActionButtonItem
            {
                Text = "Create",
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new EditProfilePage(profileService));
                })
            };

            Content = listView;

            Setup(listView, profileService);
        }

        private async Task Setup(ListView listView, IProfileService profileService)
        {
            // TODO need to dispose of this eventually.
            (await profileService.GetProfilesAsync()).Subscribe(updatedList => {
                LogUtil.Debug("New list count = " + updatedList.Count);
                listView.ItemsSource = updatedList.Select(p => new ProfileBinder(p));
            });
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
