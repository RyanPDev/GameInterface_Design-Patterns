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
        loginButton.gameObject.SetActive(true);
        viewModel
            .IsLogged
            .Subscribe((IsLogged) =>
            {
                if(IsLogged)
                    SceneManager.LoadScene(1);
            }).AddTo(_disposables);


        loginButton.onClick.AddListener(() =>
        {
            _viewModel.LoginButtonPressed.Execute();
        }
    );
    }
}