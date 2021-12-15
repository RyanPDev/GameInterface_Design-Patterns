using UnityEngine;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;

public class SettingsPanelView : View
{
    private SettingsPanelViewModel _viewModel;
    [SerializeField] private Button createButton;
    [SerializeField] private Button signInButton;

    [SerializeField] private Toggle notificationsToggle;
    [SerializeField] private Toggle audioToggle;

    public void SetViewModel(SettingsPanelViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.IsVisible.Subscribe((isVisible) =>
        {
            gameObject.SetActive(isVisible);
            gameObject.GetComponent<RectTransform>().DOLocalMoveX(5, 0f);
            gameObject.GetComponent<RectTransform>().DOMoveX(0, .2f);
        });

        //Panels
        _viewModel.IsLoginVisible.Subscribe((isVisible) =>
        {
            signInButton.gameObject.SetActive(isVisible);

        });

        _viewModel.IsCreateVisible.Subscribe((isVisible) =>
        {
            createButton.gameObject.SetActive(isVisible);
        });

        //Buttons
        createButton.onClick.AddListener(() =>
        {
            _viewModel.OnCreateAccountButtonPressed.Execute();
        });

        signInButton.onClick.AddListener(() =>
        {
            _viewModel.OnSignInButtonPressed.Execute();
        });

        //Toggles
        audioToggle.onValueChanged.AddListener((value) =>
        {
            _viewModel.IsAudioOn.Value = value;
        });

        notificationsToggle.onValueChanged.AddListener((value) =>
        {
            _viewModel.IsNotificationsOn.Value = value;
        });
    }
}