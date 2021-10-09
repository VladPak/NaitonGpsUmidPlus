using Foundation;
using NaitonGps.Controls;
using NaitonGps.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderedEntryTransparent), typeof(CustomBorderedEntryTransparent))]
namespace NaitonGps.iOS.Renderers
{
    class CustomBorderedEntryTransparent : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                //Control.BackgroundColor = UIColor.FromRGB(204, 153, 255);
                Control.BorderStyle = UITextBorderStyle.None;
                //Control.Layer.CornerRadius = (nfloat)9;
                //Control.Layer.BorderWidth = 1;
                Control.Layer.MasksToBounds = true;
                //Control.Layer.BorderColor = Color.FromHex("#E7E8E7").ToCGColor();
                Control.Layer.BorderColor = Color.FromHex("#00000000").ToCGColor();
                //Control.TintColor = UIColor.FromRGB(38, 134, 70);
            }
        }
    }
}