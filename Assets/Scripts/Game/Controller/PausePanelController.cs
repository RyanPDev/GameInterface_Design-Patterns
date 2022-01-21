using UniRx;
using UnityEngine;

class PausePanelController : Controller
{
    public PausePanelController(PausePanelViewModel viewModel, ChangeSceneUseCase changeSceneUseCase)
    {
        viewModel.OnResumeButtonPressed.Subscribe((_) =>
        {
            viewModel.IsVisible.Value = false;
            Time.timeScale = 1;
        }).AddTo(_disposables);

        viewModel.OnMenuButtonPressed.Subscribe((_) =>
        {
            changeSceneUseCase.ChangeScene(1);
            Time.timeScale = 1;
        }).AddTo(_disposables);
    }
}