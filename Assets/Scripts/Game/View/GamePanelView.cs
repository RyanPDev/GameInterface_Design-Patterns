using UnityEngine;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class GamePanelView : View
{
    private GamePanelViewModel viewModel;

    [SerializeField] private Button pauseButton;

    public void SetViewModel(GamePanelViewModel _viewModel)
    {
        viewModel = _viewModel;

        //pause
        pauseButton.onClick.AddListener(() =>
        {
            viewModel.PauseButtonPressed.Execute();
        });
    }
}
