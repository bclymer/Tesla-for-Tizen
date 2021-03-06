﻿using System.Collections.Generic;
using System;
using System.Linq;
using TeslaTizen.Models;
using TeslaTizen.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using TeslaTizen.Utils;
using System.Threading.Tasks;
using TeslaTizen.Data;

namespace TeslaTizen.Pages
{
    public class ProfilesListPage: CirclePage
    {
        private IDisposable Disposable;

        public ProfilesListPage(TeslaVehicle vehicle, IProfileService profileService, ITeslaAPIWrapper teslaAPIWrapper)
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
                await Navigation.PushAsync(new ProfileActionPage(binder.Profile, vehicle, profileService, teslaAPIWrapper));
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

            Disposable = profileService
                .GetProfiles()
                .Subscribe(updatedList => {
                    LogUtil.Debug("New profiles list count = " + updatedList.Count);
                    listView.ItemsSource = updatedList.Select(p => new ProfileBinder(p));
                });
        }

        protected override bool OnBackButtonPressed()
        {
            Disposable?.Dispose();
            return base.OnBackButtonPressed();
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
