public class AccountManagerUseCase : UseCase, IAccountManagerUseCase
{
    readonly IEventDispatcherService eventDispatcherService;
    readonly IFirebaseAccountService firebaseAccountService;

    public AccountManagerUseCase(IFirebaseAccountService _firebaseAccountService, IEventDispatcherService _eventDispatcherService)
    {
        firebaseAccountService = _firebaseAccountService;
        eventDispatcherService = _eventDispatcherService;
    }

    public void SignIn(string mail, string pass)
    {
        firebaseAccountService.SignIn(mail, pass);
        //eventDispatcherService.Dispatch(new SignInEvent(mail,pass));    
    }

    public void CreateAccount(string mail, string pass)
    {
        firebaseAccountService.Create(mail, pass);
        //eventDispatcherService.Dispatch(new CreateAccountEvent(mail,pass));
    }    
}