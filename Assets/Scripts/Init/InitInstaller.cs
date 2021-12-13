using UnityEngine;

public class InitInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform canvasParent;
    [SerializeField] private LoginView loginPrefab;

    FirebaseLoginService firebaseLoginService;

    LoginUseCase loginUseCase;

    private void Awake()
    {
        var userRepository = new UserRepository();
        var eventDispatcherService = new EventDispatcherService();
        firebaseLoginService = new FirebaseLoginService(eventDispatcherService);
        new FirebaseAccountService(eventDispatcherService);

        ServiceLocator.Instance.RegisterService<IUserDataAccess>(userRepository);
        ServiceLocator.Instance.RegisterService<IEventDispatcherService>(eventDispatcherService);
        //ServiceLocator.Instance.RegisterService<SceneHandlerService>(sceneHandlerService);
        //ServiceLocator.Instance.RegisterService<DatabaseService>(databaseService);
        //ServiceLocator.Instance.RegisterService<AuthenticationService>(firebaseAuthenticationService);

        var loginView = Instantiate(loginPrefab, canvasParent);
        var loginViewModel = new LoginViewModel();
        loginView.SetViewModel(loginViewModel);

        loginUseCase = new LoginUseCase(firebaseLoginService, eventDispatcherService);

        new UpdateUsernameUseCase(userRepository, eventDispatcherService);

        new ChangeSceneUseCase(eventDispatcherService);
        new LoginController(loginViewModel, loginUseCase);
        new LoginPresenter(loginViewModel, eventDispatcherService);
    }

    private void Start()
    {
        firebaseLoginService.Init();
    }
}