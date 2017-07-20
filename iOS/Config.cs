using Xamarin.Forms;

[assembly: Dependency(typeof(NutraBiotics.iOS.Config))]

namespace NutraBiotics.iOS
{
    using System;
    using Interfaces;
	using SQLite.Net.Interop;

	public class Config : IConfig
    {
		string directoryDB;
		ISQLitePlatform platform;

		public string DirectoryDB
		{
			get
			{
				if (string.IsNullOrEmpty(directoryDB))
				{
					var directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					directoryDB = System.IO.Path.Combine(directory, "..", "Library");
				}

				return directoryDB;
			}
		}

		public ISQLitePlatform Platform
		{
			get
			{
				if (platform == null)
				{
					platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
				}

				return platform;
			}
		}
	}
}