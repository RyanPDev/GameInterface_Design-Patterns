using UnityEngine;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GamePanelView : View
{
    private GamePanelViewModel viewModel;

    [SerializeField] private Button pauseButton;
    [SerializeField] private RectTransform lettersLayout;

    [SerializeField] private LetterView _letterViewPrefab;
    private List<LetterView> letters;

    public void SetViewModel(GamePanelViewModel _viewModel)
    {
        viewModel = _viewModel;

        letters = new List<LetterView>();

        viewModel.letter.ObserveAdd().Subscribe(SetLetters).AddTo(_disposables);

        //pause
        pauseButton.onClick.AddListener(() =>
        {
            viewModel.PauseButtonPressed.Execute();
        });
    }

    private void SetLetters(CollectionAddEvent<LetterViewModel> _viewModel)
    {
        var letterView = Instantiate(_letterViewPrefab, lettersLayout);
        letterView.SetViewModel(_viewModel.Value);

        letters.Add(letterView);
    }
}