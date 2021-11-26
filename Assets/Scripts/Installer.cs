using UnityEngine;

public class Installer : MonoBehaviour
{
    [SerializeField] private RectTransform canvasParent;
    [SerializeField] private LoginView loginPrefab;

    FirebaseLoginService firebaseLoginService;
    private void Awake()
    {
        var loginView = Instantiate(loginPrefab, canvasParent);

        var loginViewModel = new LoginViewModel();
        loginView.SetViewModel(loginViewModel);

        var eventDispatcherService = new EventDispatcherService();
        firebaseLoginService = new FirebaseLoginService(eventDispatcherService);

        var loginUseCase = new LoginUseCase(firebaseLoginService, eventDispatcherService);

        new LoginController(loginViewModel, loginUseCase);

        new LoginPresenter(loginViewModel, loginUseCase, eventDispatcherService);
    }

    private void Start()
    {
        firebaseLoginService.Init();
    }
}
