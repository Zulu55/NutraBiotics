using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace NutraBiotics.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            UISwitch.Appearance.OnTintColor = UIColor.FromRGB(81, 83, 106);
            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();

			LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
