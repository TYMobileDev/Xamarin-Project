using System;
namespace PacificCoral
{
	public class LostSaleDetaileItem
	{
		public string Customer { get; set; }
		public int Start { get; set; }
		public int End { get; set; }
		public int Change
		{
			get { return End - Start; }
		}
	}
}
