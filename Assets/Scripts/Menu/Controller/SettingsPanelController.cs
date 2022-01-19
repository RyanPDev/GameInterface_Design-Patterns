using UniRx;

class SettingsPanelController : Controller
{
    private readonly SettingsPanelViewModel settingsPanelViewModel;
    private readonly SignInPanelViewModel signInViewModel;

    private readonly IUpdateUserUseCase updateUserUseCase;
    private readonly IAccountManagerUseCase accountManagerUseCase;

    public SettingsPanelController(SettingsPanelViewModel _viewModel, SignInPanelViewModel _signInViewModel, IUpdateUserUseCase _updateUserUseCase, IAccountManagerUseCase _accountManagerUseCase)
    {
        updateUserUseCase = _updateUserUseCase;
        accountManagerUseCase = _accountManagerUseCase;
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
            .OnSignOutButtonPressed
            .Subscribe((_) =>
            {
                accountManagerUseCase.SignOut();

                settingsPanelViewModel.IsLoginVisible.Value = true;
                settingsPanelViewModel.IsCreateVisible.Value = true;
                settingsPanelViewModel.IsSignOutVisible.Value = false;
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
            .OnAudioClicked
            .Subscribe((isOn) =>
            {
                updateUserUseCase.UpdateAudio(isOn);
            })
            .AddTo(_disposables);

        settingsPanelViewModel
            .OnNotificationClicked
            .Subscribe((isOn) =>
            {
                updateUserUseCase.UpdateNotifications(isOn);
            })
            .AddTo(_disposables);
    }
}
