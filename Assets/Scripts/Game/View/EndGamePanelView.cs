using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

class EndGamePanelView : View
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI ButtonText;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private Image victoryScreen;
    [SerializeField] private Image defeatScreen;

    private EndGamePanelViewModel viewModel;
    private bool firstTime;

    public void SetViewModel(EndGamePanelViewModel _viewModel)
    {
        viewModel = _viewModel;
        firstTime = true;
        viewModel
            .IsVisible
            .Subscribe((isVisible) =>
            {
                if (!isVisible)
                    Time.timeScale = 1;
               

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
                   continueButton.gameObject.SetActive(true);

               }
               else
               {
                   ButtonText.SetText("RETRY (AD)");
                   defeatScreen.enabled = true;
                   victoryScreen.enabled = false;
                   if (!firstTime)
                   {
                       continueButton.gameObject.SetActive(false);
                   }
                       

               }
           })
           .AddTo(_disposables);
        viewModel.score.Subscribe((winStreak) =>
        {
            Score.text = "Score: " + winStreak * 100;

        }).AddTo(_disposables);
        viewModel.timer.Subscribe((_time) =>
        {
            Timer.text = "Time: " + _time;
        }).AddTo(_disposables);
        continueButton.onClick.AddListener(() =>
        {
            _viewModel.OnContinueButtonPressed.Execute();
            firstTime = false;
        });

        menuButton.onClick.AddListener(() =>
        {
            _viewModel.OnMenuButtonPressed.Execute();
        });
    }
}