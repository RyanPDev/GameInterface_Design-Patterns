﻿using UniRx;

class SettingsPanelController : Controller
{
    private readonly SettingsPanelViewModel settingsPanelViewModel;
    private readonly SignInPanelViewModel signInViewModel;

    private readonly IUpdateUserUseCase updateUserUseCase;

    public SettingsPanelController(SettingsPanelViewModel _viewModel, SignInPanelViewModel _signInViewModel, IUpdateUserUseCase _updateUserUseCase)
    {
        updateUserUseCase = _updateUserUseCase;
        settingsPanelViewModel = _viewModel;
        signInViewModel = _signInViewModel;

        settingsPanelViewModel
            .OnSignInButtonPressed
            .Subscribe((_) =>
            {
                signInViewModel.IsVisible.Value = true;
                signInViewModel.signInAction.Value = true;
            })
            .AddTo(_disposables);

        settingsPanelViewModel
            .OnCreateAccountButtonPressed
            .Subscribe((_) =>
            {
                signInViewModel.IsVisible.Value = true;
                signInViewModel.signInAction.Value = false;
            })
            .AddTo(_disposables);

        settingsPanelViewModel
            .IsAudioOn
            .Subscribe((isOn) =>
            {
                updateUserUseCase.UpdateAudio(isOn);
            });

        settingsPanelViewModel
            .IsNotificationsOn
            .Subscribe((isOn) =>
            {
                updateUserUseCase.UpdateNotifications(isOn);
            });
    }
}
