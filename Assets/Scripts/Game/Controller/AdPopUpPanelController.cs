using UniRx;
using UnityEngine;

class AdPopUpPanelController : Controller
{
    public AdPopUpPanelController(AdPopUpPanelViewModel viewModel, ChangeSceneUseCase changeSceneUseCase)
    {
        viewModel.OnAdButtonPressed.Subscribe((_) =>
        {

            Time.timeScale = 1;
        }).AddTo(_disposables);

        viewModel.OnMenuButtonPressed.Subscribe((_) =>
        {
            changeSceneUseCase.ChangeScene(1);
            Time.timeScale = 1;
        }).AddTo(_disposables);
    }
}