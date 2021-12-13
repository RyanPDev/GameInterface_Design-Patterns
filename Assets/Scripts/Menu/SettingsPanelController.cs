using UniRx;

class SettingsPanelController : Controller
{
    private readonly SettingsPanelViewModel settingsPanelViewModel;
    private readonly SignInPanelViewModel signInViewModel;

    public SettingsPanelController(SettingsPanelViewModel _viewModel, SignInPanelViewModel _signInViewModel)
    {
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
    }
}
