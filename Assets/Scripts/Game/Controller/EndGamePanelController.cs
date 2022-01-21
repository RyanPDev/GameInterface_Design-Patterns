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
                //Nuevo juego if gameResult = true
                gamePanelViewModel.newGame.Value = true;
                updateGameUseCase.NewGame();
            }
            else
            {
                //Reiniciar juego if gameResult = false

            }


        }).AddTo(_disposables);

        viewModel.OnMenuButtonPressed.Subscribe((taskText) =>
        {
            changeSceneUseCase.ChangeScene(1);
        }).AddTo(_disposables);
    }
}