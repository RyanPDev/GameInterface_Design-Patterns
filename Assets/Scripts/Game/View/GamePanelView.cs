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

    [SerializeField] public RectTransform loadingScreen;

    [SerializeField] private TextMeshProUGUI wordText;

    //the total time of the animation
    public float repeatTime = 1;

    //the time for a dot to bounce up and come back down
    public float bounceTime = 0.25f;

    //how far does each dot move
    public float bounceHeight = 10;

    public List<GameObject> dots;
    public CanvasGroup canvasGroup;

    public void SetViewModel(GamePanelViewModel _viewModel)
    {
        viewModel = _viewModel;

        if (repeatTime < dots.Count * bounceTime)
        {
            repeatTime = dots.Count * bounceTime;
        }
        InvokeRepeating("Animate", 0, repeatTime);
        canvasGroup.DOFade(1, 0.2f);

        letters = new List<LetterView>();

        viewModel.letter.ObserveAdd().Subscribe(SetLetters).AddTo(_disposables);

        //pause
        pauseButton.onClick.AddListener(() =>
        {
            viewModel.PauseButtonPressed.Execute();
        });

        viewModel.word.Subscribe((word) =>
        {
            wordText.text = word;
        })
        .AddTo(_disposables);
    }

    private void SetLetters(CollectionAddEvent<LetterViewModel> _viewModel)
    {
        var letterView = Instantiate(_letterViewPrefab, lettersLayout);
        letterView.SetViewModel(_viewModel.Value);

        letters.Add(letterView);
    }

    void Animate() // Codigo de https://gist.github.com/reidscarboro/588911e7bc0e0ad82bfa8a1ad2397bd5
    {
        for (int i = 0; i < dots.Count; i++)
        {
            int dotIndex = i;

            dots[dotIndex].transform
                .DOMoveY(dots[dotIndex].transform.position.y + bounceHeight, bounceTime / 2)
                .SetDelay(dotIndex * bounceTime / 2)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    dots[dotIndex].transform
                        .DOMoveY(dots[dotIndex].transform.position.y - bounceHeight, bounceTime / 2)
                        .SetEase(Ease.InQuad);
                });
        }
    }
}