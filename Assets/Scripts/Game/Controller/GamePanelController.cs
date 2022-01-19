using UniRx;
using UnityEngine;

class GamePanelController : Controller
{
    private readonly GamePanelViewModel gamePanelViewModel;
    private readonly PausePanelViewModel pausePanelViewModel;
    
    public GamePanelController(GamePanelViewModel _viewModel, PausePanelViewModel _pausePanelViewModel)
    {
        gamePanelViewModel = _viewModel;
        pausePanelViewModel = _pausePanelViewModel;

        gamePanelViewModel
            .PauseButtonPressed
            .Subscribe((_) =>
            {
                pausePanelViewModel.IsVisible.Value = true;
                Time.timeScale = 0;
            })
            .AddTo(_disposables);
    }
}