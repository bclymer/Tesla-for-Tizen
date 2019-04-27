using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeslaTizen.Utils;
using Tizen.Applications;

namespace TeslaTizen.Widget
{
    public class TeslaWidget : WidgetApplication
    {
        public TeslaWidget(Type type) : base(type)
        {
            LogUtil.Debug("Launching WidgetApplication");
        }
    }
}
