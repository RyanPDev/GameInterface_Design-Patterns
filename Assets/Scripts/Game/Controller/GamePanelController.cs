using UniRx;
using UnityEngine;

class GamePanelController : Controller
{
    private readonly GamePanelViewModel gamePanelViewModel;
    private readonly PausePanelViewModel pausePanelViewModel;
    private readonly EndGamePanelViewModel endGamePanelViewModel;
    
    public GamePanelController(GamePanelViewModel _viewModel, PausePanelViewModel _pausePanelViewModel, EndGamePanelViewModel _endGamePanelViewModel)
    {
        gamePanelViewModel = _viewModel;
        pausePanelViewModel = _pausePanelViewModel;
        endGamePanelViewModel = _endGamePanelViewModel;

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