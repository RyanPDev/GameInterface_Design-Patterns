using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonsView : MonoBehaviour
{
    private ButtonsViewModel _viewModel;

    [SerializeField] private Button homeButton;
    [SerializeField] private Button scoreButton;
    [SerializeField] private Button settingsButton;

    public void SetViewModel(ButtonsViewModel viewModel)
    {
        _viewModel = viewModel;

        homeButton.GetComponent<Image>().DOColor(new Color(0, 0.25f, 1), 0);
        scoreButton.GetComponent<Image>().DOColor(new Color(0.64706f, 0.93725f, 0.98039f), 0);
        settingsButton.GetComponent<Image>().DOColor(new Color(0.64706f, 0.93725f, 0.98039f), 0);

        homeButton.onClick.AddListener(() =>
        {
            homeButton.GetComponent<Image>().DOColor(new Color(0, 0.25f, 1), .2f);
            scoreButton.GetComponent<Image>().DOColor(new Color(0.64706f, 0.93725f, 0.98039f), .2f);
            settingsButton.GetComponent<Image>().DOColor(new Color(0.64706f, 0.93725f, 0.98039f), .2f);
            _viewModel.OnHomeButtonPressed.Execute();
        });
        scoreButton.onClick.AddListener(() =>
        {
            homeButton.GetComponent<Image>().DOColor(new Color(0.64706f, 0.93725f, 0.98039f), .2f);
            scoreButton.GetComponent<Image>().DOColor(new Color(0, 0.25f, 1), .2f);
            settingsButton.GetComponent<Image>().DOColor(new Color(0.64706f, 0.93725f, 0.98039f), .2f);
            _viewModel.OnScoreButtonPressed.Execute();
        });
        settingsButton.onClick.AddListener(() =>
        {
            homeButton.GetComponent<Image>().DOColor(new Color(0.64706f, 0.93725f, 0.98039f), .2f);
            scoreButton.GetComponent<Image>().DOColor(new Color(0.64706f, 0.93725f, 0.98039f), .2f);
            settingsButton.GetComponent<Image>().DOColor(new Color(0, 0.25f, 1), .2f);
            _viewModel.OnSettingsButtonPressed.Execute();
        });
    }
}