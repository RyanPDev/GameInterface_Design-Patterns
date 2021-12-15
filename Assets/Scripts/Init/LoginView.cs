using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using DG.Tweening;
using System.Collections.Generic;

public class LoginView : View
{
    [SerializeField] private Button loginButton;
    [SerializeField] private TextMeshProUGUI ID;

    private LoginViewModel viewModel;

    //the total time of the animation
    public float repeatTime = 1;

    //the time for a dot to bounce up and come back down
    public float bounceTime = 0.25f;

    //how far does each dot move
    public float bounceHeight = 10;

    public List<GameObject> dots;

    public CanvasGroup canvasGroup;

    public void SetViewModel(LoginViewModel _viewModel)
    {
        viewModel = _viewModel;
        viewModel.isVisible.Value = true;
        loginButton.gameObject.SetActive(false);

        viewModel
            .isVisible
            .Subscribe((IsVisible) =>
            {
                loginButton.gameObject.SetActive(IsVisible);
                if (!IsVisible)
                {
                    if (repeatTime < dots.Count * bounceTime)
                    {
                        repeatTime = dots.Count * bounceTime;
                    }
                    InvokeRepeating("Animate", 0, repeatTime);
                    canvasGroup.DOFade(1, 0.2f);
                }

            }).AddTo(_disposables);

        loginButton.onClick.AddListener(() =>
        {
            _viewModel.LoginButtonPressed.Execute();
        }
    );
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