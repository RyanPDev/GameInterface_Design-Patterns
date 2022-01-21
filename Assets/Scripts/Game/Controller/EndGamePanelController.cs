using UniRx;

class EndGamePanelController : Controller
{
    public EndGamePanelController(EndGamePanelViewModel viewModel, GamePanelViewModel gamePanelViewModel, IChangeSceneUseCase changeSceneUseCase, IUpdateGameUseCase updateGameUseCase)
    {
        viewModel.OnContinueButtonPressed.Subscribe((_) =>
        {
           
            viewModel.IsVisible.Value = false;
            if (viewModel.gameResult.Value)
            {
                gamePanelViewModel.newGame.Value = true;
                updateGameUseCase.NewGame();
            }
            else
            {

            }

        }).AddTo(_disposables);

        viewModel.OnMenuButtonPressed.Subscribe((taskText) =>
        {
            changeSceneUseCase.ChangeScene(1);
        }).AddTo(_disposables);
    }
}