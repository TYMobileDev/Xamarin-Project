using System;
using System.Collections.Generic;

namespace PacificCoral
{
	public static class DictionaryExtensions
	{
		public static T Get<T>(this IDictionary<string, object> self, string key)
		{
			var ret = default(T);
			if (self != null && self.ContainsKey(key))
				ret = (T)self[key];

			return ret;
		}
	}
}
