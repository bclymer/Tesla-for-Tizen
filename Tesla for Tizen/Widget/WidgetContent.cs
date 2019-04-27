using ElmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeslaTizen.Utils;
using Tizen.Applications;

namespace TeslaTizen.Widget
{
    public class WidgetContent: WidgetBase
    {
        public override void OnCreate(Bundle content, int w, int h)
        {
            try
            {
                base.OnCreate(content, w, h);
                Conformant conformant = new Conformant(Window);
                conformant.Show();
                Scroller scroller = new Scroller(Window)
                {
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1,
                    ScrollBlock = ScrollBlock.None,
                };
                scroller.Show();

                Box box = new Box(Window)
                {
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1,
                };
                box.Show();
                scroller.SetContent(box);
                conformant.SetContent(scroller);

                Button exitButton = new Button(Window)
                {
                    Text = "Exit Test",
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1
                };
                box.PackEnd(exitButton);
                exitButton.Show();
            }
            catch (Exception e)
            {
                LogUtil.Warn("exception " + e);
            }
        }

        public override void OnPause()
        {
            base.OnPause();
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnResize(int w, int h)
        {
            base.OnResize(w, h);
        }

        public override void OnUpdate(Bundle content, bool isForce)
        {
            base.OnUpdate(content, isForce);
        }

        public override void OnDestroy(WidgetDestroyType reason, Bundle content)
        {
            base.OnDestroy(reason, content);
        }
    }
}
