using UnityEngine;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;

public class SettingsPanelView : View
{
    private SettingsPanelViewModel _viewModel;
    [SerializeField] private Button createButton;
    [SerializeField] private Button signInButton;

    public void SetViewModel(SettingsPanelViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.IsVisible.Subscribe((isVisible) =>
        {
            gameObject.SetActive(isVisible);
            gameObject.GetComponent<RectTransform>().DOLocalMoveX(5, 0f);
            gameObject.GetComponent<RectTransform>().DOMoveX(0, .2f);
        });

        createButton.onClick.AddListener(() =>
        {
            _viewModel.OnCreateAccountButtonPressed.Execute();
        });

        signInButton.onClick.AddListener(() =>
        {
            _viewModel.OnSignInButtonPressed.Execute();
        });
    }
}