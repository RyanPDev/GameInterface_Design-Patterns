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
    public List<LetterView> letters;

    [SerializeField] public RectTransform loadingScreen;

    [SerializeField] private TextMeshProUGUI wordText;

    //the total time of the animation
    public float repeatTime = 1;

    //the time for a dot to bounce up and come back down
    public float bounceTime = 0.25f;

    //how far does each dot move
    public float bounceHeight = 10;
    public Transform HangedMan;
    public List<GameObject> dots;
    public List<Image> Lifes = new List<Image>();
    public CanvasGroup canvasGroup;
    bool firstTime = true;

    public void FixedUpdate()
    {
        
    }
    public void SetViewModel(GamePanelViewModel _viewModel)
    {
        viewModel = _viewModel;

        foreach (Transform child in HangedMan)
        {
            Lifes.Add(child.GetComponent<Image>());
        }

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
            if (word != string.Empty)
                loadingScreen.gameObject.SetActive(false);
        })
        .AddTo(_disposables);
        viewModel.wrongNumLetters.Subscribe((wrongNumLetters) =>
        {
            if (firstTime)
                firstTime = false;
            else
                Lifes[wrongNumLetters].enabled = true;


        }).AddTo(_disposables);
        viewModel.OnReset.Subscribe((_) =>
        {
            loadingScreen.gameObject.SetActive(true);
            Reset();

        }).AddTo(_disposables);

        viewModel.OnNewWord.Subscribe((_) =>
        {
            for (int i = 0; i < letters.Count; i++)
            {
                Destroy(letters[i].gameObject);
            }
            letters.Clear();

        }).AddTo(_disposables);

        viewModel.newGame.Subscribe((newGame) =>
        {
        if (newGame)
        {
            foreach (LetterView element in letters)
            {
                element.letterColor.color = new Color(1, 1, 1);
                element.letterButton.interactable = true;
            }
            viewModel.newGame.Value = false;
        }
    }).AddTo(_disposables);
    }
    public void Reset()
    {
        firstTime = true;
        viewModel.wrongNumLetters.Value = 0;
        foreach (Image i in Lifes)
        {
            i.enabled = false;
        }
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