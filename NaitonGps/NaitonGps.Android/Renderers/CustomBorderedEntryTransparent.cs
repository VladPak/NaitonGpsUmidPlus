using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NaitonGps.Controls;
using NaitonGps.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderedEntryTransparent), typeof(CustomBorderedEntryTransparent))]
namespace NaitonGps.Droid.Renderers
{
    class CustomBorderedEntryTransparent : EntryRenderer
    {
        public CustomBorderedEntryTransparent(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                //Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                ////Control.Bottom = 5;

                //int[][] states = new int[][] {
                //new int[] { -Android.Resource.Attribute.StateFocused}, // enabled
                //new int[] {Android.Resource.Attribute.StateFocused} // disabled
                //};

                //int[] colors = new int[] { Color.LightGray.ToAndroid(), Color.FromHex("#66a103").ToAndroid() };

                //ColorStateList myList = new ColorStateList(states, colors);

                //GradientDrawable gradientDrawable = new GradientDrawable();
                //gradientDrawable.SetCornerRadius(15);
                //Control.Background = gradientDrawable;
                //gradientDrawable.SetStroke(2, myList);
                Control.SetTextColor(global::Android.Graphics.Color.Black);
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Color.FromHex("#00000000").ToAndroid());
                gd.SetCornerRadius(0);
                gd.SetStroke(2, Color.FromHex("#00000000").ToAndroid());
                Control.Background = gd;
            }
        }
    }
}