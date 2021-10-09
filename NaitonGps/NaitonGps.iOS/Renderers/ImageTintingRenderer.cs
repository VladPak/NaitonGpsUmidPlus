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

[assembly: ExportRenderer(typeof(ImageTinting), typeof(ImageTintingRenderer))]
namespace NaitonGps.iOS.Renderers
{
    public class ImageTintingRenderer : ImageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            Control.TintColor = UIColor.Black;
        }
    }
}