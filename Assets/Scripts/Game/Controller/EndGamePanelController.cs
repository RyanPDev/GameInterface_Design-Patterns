using UniRx;

class EndGamePanelController : Controller
{
    public EndGamePanelController(EndGamePanelViewModel viewModel, IChangeSceneUseCase changeSceneUseCase, IUpdateGameUseCase updateGameUseCase)
    {
        viewModel.OnContinueButtonPressed.Subscribe((_) =>
        {
            if (viewModel.gameResult.Value)
            {
                //Nuevo juego if gameResult = true
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