using ElmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeslaTizen.Models;
using TeslaTizen.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace TeslaTizen.Pages
{
    public class DeleteActionPage : TwoButtonPage
    {
        public DeleteActionPage(Profile profile, VehicleAction action, IProfileService profileService)
        {
            FirstButton = new MenuItem
            {
                Icon = new FileImageSource
                {
                    File = "b_option_list_icon_delete.png",
                }
            };
        }
    }
}
