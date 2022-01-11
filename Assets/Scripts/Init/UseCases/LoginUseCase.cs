public class LoginUseCase : UseCase, ILoginUseCase
{
    private readonly IFirebaseLoginService firebaseLoginService;
    private readonly IEventDispatcherService eventDispatcherService;

    public LoginUseCase(IFirebaseLoginService _firebaseLoginService, IEventDispatcherService _eventDispatcherService)
    {
        firebaseLoginService = _firebaseLoginService;
        eventDispatcherService = _eventDispatcherService;
        eventDispatcherService.Subscribe<UserInFirebase>(CheckUser);
    }

    //public void Login()
    //{
    //    firebaseLoginService.Login();
    //}

    public override void Dispose()
    {
        base.Dispose();
        eventDispatcherService.Unsubscribe<UserInFirebase>(CheckUser);
    }

    public void CheckUser(UserInFirebase userExists)
    {
        if (userExists.existsInFirebase)
        {
            firebaseLoginService.LoadData();
        }
        else
        {
            firebaseLoginService.Login();
        }
    }

    //public void Init()
    //{
    //    firebaseLoginService.LoadData();
    //}
}