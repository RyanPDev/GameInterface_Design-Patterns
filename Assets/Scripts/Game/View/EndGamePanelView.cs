using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

class EndGamePanelView : View
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI ButtonText;

    private EndGamePanelViewModel viewModel;

    public void SetViewModel(EndGamePanelViewModel _viewModel)
    {
        viewModel = _viewModel;

        viewModel
            .IsVisible
            .Subscribe((isVisible) =>
            {
                gameObject.SetActive(isVisible);
            })
            .AddTo(_disposables);

        viewModel
           .gameResult
           .Subscribe((_signInAction) =>
           {
               if (_signInAction)
                   ButtonText.SetText("CONTINUE");
               else
                   ButtonText.SetText("RETRY");
           })
           .AddTo(_disposables);

        continueButton.onClick.AddListener(() =>
        {
            _viewModel.OnContinueButtonPressed.Execute();
        });

        menuButton.onClick.AddListener(() =>
        {
            _viewModel.OnMenuButtonPressed.Execute();
        });
    }
}