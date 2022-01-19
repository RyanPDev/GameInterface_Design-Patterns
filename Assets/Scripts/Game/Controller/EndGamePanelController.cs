using UniRx;

class EndGamePanelController : Controller
{
    public EndGamePanelController(EndGamePanelViewModel viewModel, ChangeSceneUseCase changeSceneUseCase)
    {
        viewModel.OnContinueButtonPressed.Subscribe((_) =>
        {
            if (viewModel.gameResult.Value)
            {
                //Nuevo juego if gameResult = true

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