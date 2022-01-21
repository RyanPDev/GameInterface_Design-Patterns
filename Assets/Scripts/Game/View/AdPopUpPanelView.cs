using UniRx;
using UnityEngine;
using UnityEngine.UI;

class AdPopUpPanelView : View
{
    [SerializeField] private Button AdButton;
    [SerializeField] private Button menuButton;

    private AdPopUpPanelViewModel viewModel;

    public void SetViewModel(AdPopUpPanelViewModel _viewModel)
    {
        viewModel = _viewModel;

        viewModel
            .IsVisible
            .Subscribe((isVisible) =>
            {
                gameObject.SetActive(isVisible);
            })
            .AddTo(_disposables);

        AdButton.onClick.AddListener(() =>
        {
            _viewModel.OnAdButtonPressed.Execute();
        });

        menuButton.onClick.AddListener(() =>
        {
            _viewModel.OnMenuButtonPressed.Execute();
        });
    }
}