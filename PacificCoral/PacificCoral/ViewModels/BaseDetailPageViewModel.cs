using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;

namespace PacificCoral
{
	public class BaseDetailPageViewModel<T> : BasePageViewModel, INavigationAware where T : class, new()
	{
		protected readonly INavigationService _navigationService;

		public BaseDetailPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		protected virtual void Init()
		{

		}

		#region -- Public properties --

		private DetailsMode _Mode;

		public DetailsMode Mode
		{
			get { return _Mode; }
			set { SetProperty(ref _Mode, value); }
		}

		private T _Model;

		public T Model
		{
			get { return _Model; }
			set { SetProperty(ref _Model, value); }
		}

		public ICommand StartEditingCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnStartEditingCommandAsync); }
		}

		public ICommand SaveChangesCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnSaveChangesCommandAsync); }
		}

		public ICommand BackCommand
		{
			get { return SingleExecutionCommand.FromFunc(OnBackCommandAsync); }
		}

		#endregion

		#region -- INavigationAware implementation --

		public void OnNavigatedFrom(NavigationParameters parameters)
		{

		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{

		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			var model = parameters.Get<T>(typeof(T).Name);
			SetModel(model, parameters);
		}

		#endregion

		#region -- Protected helpers --

		protected virtual void SetModel(T model, Dictionary<string, object> parameters = null)
		{
			Model = model;
			if (model == null)
			{
				Mode = DetailsMode.Add;
				Model = new T();
			}
			else
			{
				Mode = DetailsMode.View;
			}

			Init();
		}

		protected virtual Task OnStartEditingCommandAsync()
		{
			Mode = DetailsMode.Edit;
			return Task.FromResult<object>(null);
		}

		protected virtual Task OnSaveChangesCommandAsync()
		{
			Mode = DetailsMode.View;

			var type = this.GetType();
			var propertyInfoList = type.GetRuntimeProperties();
			foreach (var item in propertyInfoList)
			{
				OnPropertyChanged(new PropertyChangedEventArgs(item.Name));
			}

			return Task.FromResult<object>(null);
		}

		protected virtual Task OnBackCommandAsync()
		{
			return _navigationService.GoBackAsync();
		}

		#endregion
	}

	public enum DetailsMode
	{
		View,
		Edit,
		Add
	}
}
