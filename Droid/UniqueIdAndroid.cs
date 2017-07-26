using Android.Provider;
using NutraBiotics.Droid;
using NutraBiotics.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(UniqueIdAndroid))]

namespace NutraBiotics.Droid
{
	public class UniqueIdAndroid : IDevice
	{
		public string GetIdentifier()
		{
			return Settings.Secure.GetString(
				Forms.Context.ContentResolver,
				Settings.Secure.AndroidId);
		}
	}
}