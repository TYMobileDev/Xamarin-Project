﻿using System;
namespace PacificCoral
{
	public static class StyleManager
	{
		public static T GetAppResource<T>(string key)
		{
			return (T)App.Current.Resources[key];
		}
	}
}
