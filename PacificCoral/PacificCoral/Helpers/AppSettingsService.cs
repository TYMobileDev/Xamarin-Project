using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PacificCoral
{
	public class AppSettingsService : IAppSettingsService
	{
		//private readonly ISettings _settings;

		//public AppSettingsService(ISettings settings)
		//{
		//	_settings = settings;
		//}

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


		public string LastLoggedinUser
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

		public string LastOPCO
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
