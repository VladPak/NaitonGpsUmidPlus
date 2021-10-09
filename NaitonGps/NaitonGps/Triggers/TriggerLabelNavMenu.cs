using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NaitonGps.Triggers
{
    public class TriggerLabelNavMenu : TriggerAction<Frame>
    {

        protected override void Invoke(Frame frm)
        {
             frm.BackgroundColor = Color.Red;
        }
    }
}
