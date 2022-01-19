using UniRx;

class HomePanelController : Controller
{
    private readonly HomePanelViewModel homePanelViewModel;
    private readonly ProfileViewModel profileViewModel;

    public HomePanelController(HomePanelViewModel _viewModel, ProfileViewModel _profileViewModel, ChangeSceneUseCase changeSceneUseCase)
    {
        homePanelViewModel = _viewModel;
        profileViewModel = _profileViewModel;

        homePanelViewModel
            .ProfileButtonPressed
            .Subscribe((_) =>
            {
                profileViewModel.IsVisible.Value = true;
                profileViewModel.UserName.SetValueAndForceNotify(string.Empty);
            })
            .AddTo(_disposables);

        homePanelViewModel
            .PlayButtonPressed
            .Subscribe((_) =>
            {
                changeSceneUseCase.ChangeScene(2);
            })
            .AddTo(_disposables);
    }
}