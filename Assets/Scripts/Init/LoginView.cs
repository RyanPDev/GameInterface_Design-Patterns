using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class LoginView : View
{
    [SerializeField] private Button loginButton;
    [SerializeField] private TextMeshProUGUI ID;

    private LoginViewModel viewModel;

    public void SetViewModel(LoginViewModel _viewModel)
    {
        viewModel = _viewModel;
        viewModel.isVisible.Value = true;
        //loginButton.gameObject.SetActive(true);
        //viewModel
        //    .IsLogged
        //    .Subscribe((IsLogged) =>
        //    {
        //        loginButton.gameObject.SetActive(!IsLogged);
        //    }).AddTo(_disposables);

        viewModel
            .isVisible
            .Subscribe((IsLogged) =>
            {
                loginButton.gameObject.SetActive(IsLogged);

            }).AddTo(_disposables);

        loginButton.onClick.AddListener(() =>
        {
            _viewModel.LoginButtonPressed.Execute();
        }
    );
    }
}