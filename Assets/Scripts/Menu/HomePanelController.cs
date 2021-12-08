using UniRx;

class HomePanelController : Controller
{
    private readonly HomePanelViewModel homePanelViewModel;

    public HomePanelController(HomePanelViewModel _viewModel)
    {
        homePanelViewModel = _viewModel;
        homePanelViewModel
            .ProfileButtonPressed
            .Subscribe((_) =>
            {

            })
            .AddTo(_disposables);

    }

}