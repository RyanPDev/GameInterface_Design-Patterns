using UnityEngine;
using System;
using System.Collections.Generic;
using UniRx;

public class InitInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform canvasParent;
    [SerializeField] private LoginView loginPrefab;

    FirebaseLoginService firebaseLoginService;

    LoginUseCase loginUseCase;

    private List<IDisposable> _disposables = new List<IDisposable>();
    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }

    private void Awake()
    {
        var userRepository = new UserRepository();
        var eventDispatcherService = new EventDispatcherService();

        firebaseLoginService = new FirebaseLoginService(eventDispatcherService);
        FirebaseAccountService firebaseAccountService = new FirebaseAccountService(eventDispatcherService, userRepository);
        var realTimeDatabaseService = new RealTimeDatabaseService(eventDispatcherService);

        ServiceLocator.Instance.RegisterService<IUserDataAccess>(userRepository);
        ServiceLocator.Instance.RegisterService<IEventDispatcherService>(eventDispatcherService);
        ServiceLocator.Instance.RegisterService<IFirebaseLoginService>(firebaseLoginService);
        ServiceLocator.Instance.RegisterService<IFirebaseAccountService>(firebaseAccountService);
        ServiceLocator.Instance.RegisterService<IRealTimeDatabaseService>(realTimeDatabaseService);

        var loginView = Instantiate(loginPrefab, canvasParent);
        var loginViewModel = new LoginViewModel().AddTo(_disposables);
        loginView.SetViewModel(loginViewModel);

        loginUseCase = new LoginUseCase(firebaseLoginService, eventDispatcherService).AddTo(_disposables);

        new UpdateUserUseCase(firebaseLoginService, userRepository, eventDispatcherService).AddTo(_disposables);

        new ChangeSceneUseCase(eventDispatcherService).AddTo(_disposables);
        new LoginController(loginViewModel, loginUseCase).AddTo(_disposables);
        new LoginPresenter(loginViewModel, eventDispatcherService).AddTo(_disposables);
    }

    private void Start()
    {
        firebaseLoginService.InitAsync();
    }
}