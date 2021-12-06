using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using UnityEngine.SceneManagement;

public class LoginView : View
{
    [SerializeField] private Button loginButton;
    [SerializeField] private TextMeshProUGUI ID;

    private LoginViewModel viewModel;

    public void SetViewModel(LoginViewModel _viewModel)
    {
        viewModel = _viewModel;

        viewModel
            .IsVisible
            .Subscribe((isVisible) =>
            {
                loginButton.gameObject.SetActive(isVisible);
                ID.gameObject.SetActive(!isVisible);
            }).AddTo(_disposables);

        viewModel
            .TextID
            .Subscribe((textID) =>
            {
                ID.SetText(textID);
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
            }).AddTo(_disposables);

        loginButton.onClick.AddListener(() =>
        {
            _viewModel.LoginButtonPressed.Execute();
        }
    );
    }
}