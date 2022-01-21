using UniRx;

class EndGamePanelController : Controller
{
    public EndGamePanelController(EndGamePanelViewModel viewModel, GamePanelViewModel gamePanelViewModel, IChangeSceneUseCase changeSceneUseCase, IUpdateGameUseCase updateGameUseCase)
    {
        viewModel.OnContinueButtonPressed.Subscribe((_) =>
        {
            viewModel.IsVisible.Value = false;
            gamePanelViewModel.newGame.Value = true;
            updateGameUseCase.NewGame();
            //if (viewModel.gameResult.Value)
            //{
            //    //Nuevo juego if gameResult = true
            //}
            //else
            //{
            //    //Reiniciar juego if gameResult = false

            //}

        }).AddTo(_disposables);

        viewModel.OnMenuButtonPressed.Subscribe((taskText) =>
        {
            changeSceneUseCase.ChangeScene(1);
        }).AddTo(_disposables);
    }
}