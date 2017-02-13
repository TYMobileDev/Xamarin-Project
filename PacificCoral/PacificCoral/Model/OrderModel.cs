using System;

namespace PacificCoral
{
	public class OrderModel
	{
		public int CustomerNumber { get; set; }
		public EOrderStatus Status { get; set; }
		public EOrderDeliveryStatus DeliveryStatus { get; set; }
		public DateTime PODate { get; set; }
		public DateTime ShipDate { get; set; }
	}
}
