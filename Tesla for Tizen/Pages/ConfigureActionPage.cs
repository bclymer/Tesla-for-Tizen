using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaTizen.Models;
using Tizen.Wearable.CircularUI.Forms;

namespace TeslaTizen.Pages
{
    public class ConfigureActionPage: CirclePage
    {
        public ConfigureActionPage(Profile profile, VehicleActionType action)
        {
            var actionParams = action.CustomizableParams();
        }
    }
}
