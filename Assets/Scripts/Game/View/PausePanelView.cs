using UniRx;
using UnityEngine;
using UnityEngine.UI;

class PausePanelView : View
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;

    private PausePanelViewModel viewModel;

    public void SetViewModel(PausePanelViewModel _viewModel)
    {
        viewModel = _viewModel;

        viewModel
            .IsVisible
            .Subscribe((isVisible) =>
            {
                gameObject.SetActive(isVisible);
            })
            .AddTo(_disposables);

        resumeButton.onClick.AddListener(() =>
        {
            _viewModel.OnResumeButtonPressed.Execute();
        });

        menuButton.onClick.AddListener(() =>
        {
            _viewModel.OnMenuButtonPressed.Execute();
        });
    }
}