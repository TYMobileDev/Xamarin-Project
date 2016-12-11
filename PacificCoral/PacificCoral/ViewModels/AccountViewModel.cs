using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;
using System.IO;
using Plugin.Media.Abstractions;
using FFImageLoading;

namespace PacificCoral
{
	public class AccountViewModel : BaseDetailPageViewModel<AccountModel>
	{
		private readonly IMedia _mediapickerService;

		public AccountViewModel(IMedia mediapickerService, INavigationService navigationService) : base(navigationService)
		{
			_mediapickerService = mediapickerService;
			UpdatePhoto();
			Title = "Account";
		}

		#region -- Public properties --

		private ImageSource _Photo;

		public ImageSource Photo
		{
			get { return _Photo; }
			set { SetProperty(ref _Photo, value); }
		}

		public ICommand ChangePhotoCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnChangePhotoCommandAsync); }
		}

		#endregion

		#region -- Overrides --

		protected override void SetModel(AccountModel model, Dictionary<string, object> parameters = null)
		{
			base.SetModel(model, parameters);
			UpdatePhoto();
		}

		protected override async Task OnSaveChangesCommandAsync()
		{
			
		}

		#endregion

		#region -- Private helpers --

		private async Task OnChangePhotoCommandAsync()
		{
			var file = await _mediapickerService.PickPhotoAsync();
			if (file == null)
				return;
			var stream = file.GetStream();
			file.Dispose();
			var buffer = new byte[stream.Length];
			await stream.ReadAsync(buffer, 0, buffer.Length);
			Model.Photo = buffer;
			UpdatePhoto();

		}

		private void UpdatePhoto()
		{
			if (Model != null && Model.Photo != null)
				Photo = ImageSource.FromStream(() => new MemoryStream(Model.Photo));
			else
				Photo = "uploadImage";
		}

		#endregion
	}
}
