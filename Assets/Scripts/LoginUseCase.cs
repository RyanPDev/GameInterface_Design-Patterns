public class LoginUseCase : ILoginUseCase
{
    private readonly IFirebaseLoginService firebaseLoginService;
    private readonly IEventDispatcherService eventDispatcherService;

    public LoginUseCase(IFirebaseLoginService _firebaseLoginService, IEventDispatcherService _eventDispatcherService)
    {
        firebaseLoginService = _firebaseLoginService;
        eventDispatcherService = _eventDispatcherService;
    }

    public void Login()
    {
        firebaseLoginService.Login();
        var logEvent = new LogEvent(firebaseLoginService.GetID());
        eventDispatcherService.Dispatch<LogEvent>(logEvent);
    }
    public bool UserExists()
    {
        return firebaseLoginService.IDAppExist();
    }
}