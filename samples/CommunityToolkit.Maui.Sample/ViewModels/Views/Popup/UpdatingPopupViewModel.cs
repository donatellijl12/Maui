﻿using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CommunityToolkit.Maui.Sample.ViewModels.Views;

public partial class UpdatingPopupViewModel : BaseViewModel
{
	const double finalUpdateProgressValue = 1;
	readonly IPopupService popupService;
	[ObservableProperty]
	string message = "";

	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(FinishCommand))]
	double updateProgress;

	public UpdatingPopupViewModel(IPopupService popupService)
	{
		this.popupService = popupService;
	}

	internal async void PerformUpdates(int numberOfUpdates)
	{
		double updateTotalForPercentage = numberOfUpdates + 1;

		for (var update = 1; update <= numberOfUpdates; update++)
		{
			Message = $"Updating {update} of {numberOfUpdates}";

			UpdateProgress = update / updateTotalForPercentage;

			await Task.Delay(TimeSpan.FromSeconds(1));
		}

		UpdateProgress = finalUpdateProgressValue;
		Message = "Updates complete";
	}

	[RelayCommand(CanExecute = nameof(CanFinish))]
	void OnFinish()
	{
		popupService.ClosePopup();
	}

	bool CanFinish() => UpdateProgress is finalUpdateProgressValue;
}