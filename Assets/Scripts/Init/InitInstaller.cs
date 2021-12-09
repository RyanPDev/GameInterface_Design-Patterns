using UnityEngine;

public class InitInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform canvasParent;
    [SerializeField] private LoginView loginPrefab;

    FirebaseLoginService firebaseLoginService;

    private void Awake()
    {
        //var sceneHandlerService = new UnitySceneHandler();
        var userRepository = new UserRepository();
        var eventDispatcherService = new EventDispatcherService();
        //var firebaseAuthenticationService = new FirebaseAuthenticationService();
        firebaseLoginService = new FirebaseLoginService(eventDispatcherService);


        ServiceLocator.Instance.RegisterService<UserDataAccess>(userRepository);
        ServiceLocator.Instance.RegisterService<IEventDispatcherService>(eventDispatcherService);
        //ServiceLocator.Instance.RegisterService<SceneHandlerService>(sceneHandlerService);
        //ServiceLocator.Instance.RegisterService<DatabaseService>(databaseService);
        //ServiceLocator.Instance.RegisterService<AuthenticationService>(firebaseAuthenticationService);


        var loginView = Instantiate(loginPrefab, canvasParent);
        var loginViewModel = new LoginViewModel();
        loginView.SetViewModel(loginViewModel);

        //var eventDispatcherService = new EventDispatcherService();
        //firebaseLoginService = new FirebaseLoginService(eventDispatcherService);

        var loginUseCase = new LoginUseCase(firebaseLoginService, eventDispatcherService);

        new ChangeSceneUseCase(eventDispatcherService);
        new LoginController(loginViewModel, loginUseCase);
        new LoginPresenter(loginViewModel, eventDispatcherService);
    }

    private void Start()
    {
        firebaseLoginService.Init();
    }
}