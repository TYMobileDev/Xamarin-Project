// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PacificCoral.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string LastLoggedinUserKey = "LastLoggedinUser";
		private static readonly string LastLoggedinUserDefault = string.Empty;
		private const string LastOPCOKey = "LastOPCO";
		private static readonly string LastOPCODefault = string.Empty;

		#endregion


		public static string LastLoggedinUser
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(LastLoggedinUserKey, LastLoggedinUserDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(LastLoggedinUserKey, value);
			}
		}

		public static string LastOPCO
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(LastOPCOKey, LastOPCODefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(LastOPCOKey, value);
			}
		}

	}
}