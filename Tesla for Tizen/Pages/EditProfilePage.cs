using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaTizen.Models;
using TeslaTizen.Services;
using TeslaTizen.Utils;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class EditProfilePage: CirclePage
    {
        private readonly IProfileService ProfileService;
        private readonly Profile Profile;

        private IDisposable Disposable;

        public EditProfilePage(IProfileService profileService) : this(Profile.Create(), profileService) { }

        public EditProfilePage(Profile profile, IProfileService profileService)
        {
            Profile = profile;
            ProfileService = profileService;

            // Insert immediately so we can monitor it's actions.
            profileService.UpsertProfileAsync(Profile);

            NavigationPage.SetHasNavigationBar(this, false);
            
            var listView = new CircleListView
            {
                Header = UIUtil.CreateHeaderLabel(profile.Name),
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
                if (binder.Type.IsRequired())
                {
                    // if the action is required you can't edit or delete it.
                    return;
                }
                await Navigation.PushAsync(new EditActionPage(profile, binder, profileService));
            };

            Content = listView;

            Disposable = ProfileService
                .GetProfile(Profile.Id)
                .Subscribe(updatedProfile => {
                    LogUtil.Debug("New actions list count = " + updatedProfile.Actions.Count);
                    // It wasn't refreshing without this.
                    listView.ItemsSource = null;
                    listView.ItemsSource = updatedProfile.Actions;
                    listView.ScrollTo(updatedProfile.Actions.Last(), ScrollToPosition.Center, true);
                    listView.Header = UIUtil.CreateHeaderLabel(updatedProfile.Name);
                });

            ToolbarItems.Add(new CircleToolbarItem
            {
                Text = "Rename",
                Icon = new FileImageSource(),
                Command = new Command(async () =>
                {
                    await Navigation.PushModalAsync(new RenameProfilePage(profile, profileService));
                })
            });

            ActionButton = new ActionButtonItem
            {
                Text = "+",
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new AddActionPage(profile, profileService));
                })
            };
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ProfileService.UpsertProfileAsync(Profile);
        }

        protected override bool OnBackButtonPressed()
        {
            Disposable?.Dispose();
            return base.OnBackButtonPressed();
        }
    }
}
