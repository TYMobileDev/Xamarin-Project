using System;

namespace PacificCoral
{
	public class AccountModel : ObservableObject
	{
		#region -- Public properties --

		private byte[] _Photo;

		public byte[] Photo
		{
			get { return _Photo; }
			set { SetProperty(ref _Photo, value); }
		}

		private string _AccountName;

		public string AccountName
		{
			get { return _AccountName; }
			set { SetProperty(ref _AccountName, value); }
		}

		private int _CustomerId;

		public int CustomerId
		{
			get { return _CustomerId; }
			set { SetProperty(ref _CustomerId, value); }
		}

		private int _AMMA;

		public int AMMA
		{
			get { return _AMMA; }
			set { SetProperty(ref _AMMA, value); }
		}

		private string _PhoneNumber;

		public string PhoneNumber
		{
			get { return _PhoneNumber; }
			set { SetProperty(ref _PhoneNumber, value); }
		}

		private string _Location;

		public string Location
		{
			get { return _Location; }
			set { SetProperty(ref _Location, value); }
		}

		#endregion
	}
}
