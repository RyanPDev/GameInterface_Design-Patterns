using UnityEngine;

public class Installer : MonoBehaviour
{
    [SerializeField] private RectTransform canvasParent;
    [SerializeField] private LoginView loginPrefab;

    private void Awake()
    {
        var loginView = Instantiate(loginPrefab, canvasParent);

        var loginViewModel = new LoginViewModel();
        loginView.SetViewModel(loginViewModel);

        var firebaseLoginService = new FirebaseLoginService();
        var eventDispatcherService = new EventDispatcherService();

        var loginUseCase = new LoginUseCase(firebaseLoginService, eventDispatcherService);

        //if (loginUseCase.UserExists())
        //{
        //    firebaseLoginService.SetData();
        //}

        //if (doLoginUseCase.UserExists()) { loginViewModel.IsVisible.Value = false; }
        //else { loginViewModel.IsVisible.Value = true; }    

        new LoginController(loginViewModel, loginUseCase);

        new LoginPresenter(loginViewModel, loginUseCase, eventDispatcherService);
    }
}
