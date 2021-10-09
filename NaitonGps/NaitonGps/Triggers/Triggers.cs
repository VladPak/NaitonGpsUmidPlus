using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NaitonGps.Triggers
{
 public class Triggers : TriggerAction<Image>
    {
        protected async override void Invoke(Image img)
        {
            await img.ScaleTo(1, 2, Easing.Linear);
        }

    }
}
