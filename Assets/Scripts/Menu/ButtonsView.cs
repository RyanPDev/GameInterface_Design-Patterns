using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonsView : MonoBehaviour
{
    private ButtonsViewModel viewModel;

    [SerializeField] private Button homeButton;
    [SerializeField] private Button scoreButton;
    [SerializeField] private Button settingsButton;

    public void SetViewModel(ButtonsViewModel _viewModel)
    {
        viewModel = _viewModel;

        SetColor(new Color(0, 0.25f, 1), new Color(0.64706f, 0.93725f, 0.98039f), new Color(0.64706f, 0.93725f, 0.98039f), 0f);

        homeButton.onClick.AddListener(() =>
        {
            SetColor(new Color(0, 0.25f, 1), new Color(0.64706f, 0.93725f, 0.98039f), new Color(0.64706f, 0.93725f, 0.98039f), .2f);
            viewModel.OnHomeButtonPressed.Execute();
        });
        scoreButton.onClick.AddListener(() =>
        {
            SetColor(new Color(0.64706f, 0.93725f, 0.98039f), new Color(0, 0.25f, 1), new Color(0.64706f, 0.93725f, 0.98039f), .2f);
            viewModel.OnScoreButtonPressed.Execute();
        });
        settingsButton.onClick.AddListener(() =>
        {
            SetColor(new Color(0.64706f, 0.93725f, 0.98039f), new Color(0.64706f, 0.93725f, 0.98039f), new Color(0, 0.25f, 1), .2f);
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