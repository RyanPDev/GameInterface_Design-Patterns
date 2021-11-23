using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class LoginView : MonoBehaviour
{
    [SerializeField] private Button _loginButton;
    //[SerializeField] private TextMeshProUGUI _loginID;

    private LoginModelView _viewModel;

    public void SetViewModel(LoginModelView viewModel)
    {
        _viewModel = viewModel;

        _viewModel
            .IsVisible
            .Subscribe((isVisible) =>
            {
                gameObject.SetActive(isVisible);
            });

        _loginButton.onClick.AddListener(() =>
        {
            Debug.Log("Listener");
            _viewModel.LoginButtonPressed.Execute();
        }
    );
    }
}