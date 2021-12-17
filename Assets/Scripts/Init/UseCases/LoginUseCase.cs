public class LoginUseCase : UseCase, ILoginUseCase
{
    private readonly IFirebaseLoginService firebaseLoginService;
    private readonly IEventDispatcherService eventDispatcherService;

    public LoginUseCase(IFirebaseLoginService _firebaseLoginService, IEventDispatcherService _eventDispatcherService)
    {
        firebaseLoginService = _firebaseLoginService;
        eventDispatcherService = _eventDispatcherService;
        eventDispatcherService.Subscribe<UserInFirebase>(AlreadyExists);
    }

    public void Login()
    {
        firebaseLoginService.Login();
    }

    public override void Dispose()
    {
        base.Dispose();
        eventDispatcherService.Unsubscribe<UserInFirebase>(AlreadyExists);
    }

    public void AlreadyExists(UserInFirebase userExists)
    {
        if (userExists.existsInFirebase)
        {
            firebaseLoginService.LoadData();
        }
    }

    public void Init()
    {
        firebaseLoginService.LoadData();
    }
}