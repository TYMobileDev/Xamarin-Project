using System;
using Xamarin.Forms;

namespace PacificCoral
{
	public static class Styles
	{
		public static string FontSFNSRegular = Device.OnPlatform("SFNSDisplayRegular", "SFNSDisplayRegular.ttf#SFNSDisplayRegular", "");
		public static string FontHNBold = Device.OnPlatform("HelveticaNeueBold", "HelveticaNeueBold.ttf#HelveticaNeueBold", "");
		public static string FontHNLight = Device.OnPlatform("HelveticaNeueLight", "HelveticaNeueLight.ttf#HelveticaNeueLight", "");
		public static string FontHNMedium = Device.OnPlatform("HelveticaNeueMedium", "HelveticaNeueMedium.ttf#HelveticaNeueMedium", "");
		public static string FontHNRegular = Device.OnPlatform("HelveticaNeueMedium", "HelveticaNeueMedium.ttf#HelveticaNeueMedium", "");
	}
}
