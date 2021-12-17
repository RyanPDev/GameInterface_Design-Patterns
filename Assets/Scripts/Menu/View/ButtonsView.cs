using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonsView : View
{
    private ButtonsViewModel viewModel;

    [SerializeField] private Button homeButton;
    [SerializeField] private Button scoreButton;
    [SerializeField] private Button settingsButton;

    public void SetViewModel(ButtonsViewModel _viewModel)
    {
        viewModel = _viewModel;

        SetColor(new Color(0.8431373f, 0.6470588f, 0.05882353f), new Color(0.8196079f, 0.7921569f, 0.7803922f), new Color(0.8196079f, 0.7921569f, 0.7803922f), 0f);

        homeButton.onClick.AddListener(() =>
        {
            SetColor(new Color(0.8431373f, 0.6470588f, 0.05882353f), new Color(0.8196079f, 0.7921569f, 0.7803922f), new Color(0.8196079f, 0.7921569f, 0.7803922f), .2f);
            viewModel.OnHomeButtonPressed.Execute();
        });
        scoreButton.onClick.AddListener(() =>
        {
            SetColor(new Color(0.8196079f, 0.7921569f, 0.7803922f), new Color(0.8431373f, 0.6470588f, 0.05882353f), new Color(0.8196079f, 0.7921569f, 0.7803922f), .2f);
            viewModel.OnScoreButtonPressed.Execute();
        });
        settingsButton.onClick.AddListener(() =>
        {
            SetColor(new Color(0.8196079f, 0.7921569f, 0.7803922f), new Color(0.8196079f, 0.7921569f, 0.7803922f), new Color(0.8431373f, 0.6470588f, 0.05882353f), .2f);
            viewModel.OnSettingsButtonPressed.Execute();
        });
    }

    private void SetColor(Color homeColor, Color scoreColor, Color settingsColor, float lerpSpeed)
    {
        homeButton.GetComponent<Image>().DOColor(homeColor, lerpSpeed);
        scoreButton.GetComponent<Image>().DOColor(scoreColor, lerpSpeed);
        settingsButton.GetComponent<Image>().DOColor(settingsColor, lerpSpeed);
    }
}