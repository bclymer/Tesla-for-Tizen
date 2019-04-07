using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TeslaTizen.Utils
{
    public static class UIUtil
    {
        public static Label CreateHeaderLabel(string text)
        {
            return new Label
            {
                HeightRequest = 120,
                Text = text,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 18,
                TextColor = Color.LightBlue,
            };
        }
    }
}
