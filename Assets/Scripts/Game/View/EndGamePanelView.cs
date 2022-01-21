using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

class EndGamePanelView : View
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI ButtonText;
    [SerializeField] private Image victoryScreen;
    [SerializeField] private Image defeatScreen;

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
           .Subscribe((victory) =>
           {
               if (victory)
               {
                   ButtonText.SetText("CONTINUE");
                   defeatScreen.enabled = false;
                   victoryScreen.enabled = true;
               }
               else
               {
                   ButtonText.SetText("RETRY");
                   defeatScreen.enabled = true;
                   victoryScreen.enabled = false;
               }
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