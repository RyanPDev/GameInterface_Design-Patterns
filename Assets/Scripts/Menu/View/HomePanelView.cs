using UnityEngine;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class HomePanelView : View
{
    private HomePanelViewModel viewModel;

    [SerializeField] private Button profileButton;
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI userNameText;

    public void SetViewModel(HomePanelViewModel _viewModel)
    {
        viewModel = _viewModel;

        viewModel.IsVisible.Subscribe((isVisible) =>
        {
            gameObject.SetActive(isVisible);
            gameObject.GetComponent<RectTransform>().DOLocalMoveX(5, 0f);
            gameObject.GetComponent<RectTransform>().DOMoveX(0, .2f);
        })
        .AddTo(_disposables);        

        viewModel.Username.Subscribe((username) =>
        {
            userNameText.text = username;
        })
        .AddTo(_disposables);

        //profile
        profileButton.onClick.AddListener(() =>
        {
            viewModel.ProfileButtonPressed.Execute();
        });

        //game
        playButton.onClick.AddListener(() =>
        {
            viewModel.PlayButtonPressed.Execute();
        });
    }
}