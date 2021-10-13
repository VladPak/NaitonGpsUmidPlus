﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Foundation;
using UIKit;

namespace NaitonGps.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            ServicePointManager
            .ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;


            //XfxControls.Init();
            //global::Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();

            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;

            foreach (var cookie in CookieStorage.Cookies)
            {
                CookieStorage.DeleteCookie(cookie);
            }

            LoadApplication(new NaitonGps.Application());
            PhantomLib.iOS.Effects.Effects.Init();
            return base.FinishedLaunching(app, options);
        }
    }
}
