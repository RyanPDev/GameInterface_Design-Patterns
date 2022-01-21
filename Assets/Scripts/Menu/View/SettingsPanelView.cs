using UnityEngine;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;

public class SettingsPanelView : View
{
    private SettingsPanelViewModel _viewModel;
    [SerializeField] private Button createButton;
    [SerializeField] private Button signInButton;
    [SerializeField] private Button signOutButton;

    [SerializeField] private Toggle notificationsToggle;
    [SerializeField] private Toggle audioToggle;

    public void SetViewModel(SettingsPanelViewModel viewModel, IUserDataAccess user)
    {
        _viewModel = viewModel;
        notificationsToggle.isOn = user.GetLocalUser().Notifications;
        audioToggle.isOn = user.GetLocalUser().Audio;

        _viewModel.IsLoginVisible.Value = !PlayerPrefs.HasKey("UserEmail");
        _viewModel.IsCreateVisible.Value = !PlayerPrefs.HasKey("UserEmail");
        _viewModel.IsSignOutVisible.Value = PlayerPrefs.HasKey("UserEmail");

        _viewModel.IsVisible.Subscribe((isVisible) =>
        {
            gameObject.SetActive(isVisible);
            gameObject.GetComponent<RectTransform>().DOLocalMoveX(5, 0f);
            gameObject.GetComponent<RectTransform>().DOMoveX(0, .2f);
        })
        .AddTo(_disposables);

        //Panels
        _viewModel.IsLoginVisible.Subscribe((isVisible) =>
        {
            signInButton.gameObject.SetActive(isVisible);

        })
        .AddTo(_disposables);

        _viewModel.IsCreateVisible.Subscribe((isVisible) =>
        {
            createButton.gameObject.SetActive(isVisible);
        })
        .AddTo(_disposables);

        _viewModel.IsSignOutVisible.Subscribe((isVisible) =>
        {
            signOutButton.gameObject.SetActive(isVisible);
        })
        .AddTo(_disposables);

        //Buttons
        createButton.onClick.AddListener(() =>
        {
            _viewModel.OnCreateAccountButtonPressed.Execute();
        });

        signInButton.onClick.AddListener(() =>
        {
            _viewModel.OnSignInButtonPressed.Execute();
        });

        signOutButton.onClick.AddListener(() =>
        {
            _viewModel.OnSignOutButtonPressed.Execute();
        });

        //Toggles
        audioToggle.onValueChanged.AddListener((value) =>
        {
            _viewModel.OnAudioClicked.Execute(value);
        });

        notificationsToggle.onValueChanged.AddListener((value) =>
        {
            _viewModel.OnNotificationClicked.Execute(value);
        });
    }
}