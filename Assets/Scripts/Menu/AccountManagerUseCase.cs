public class AccountManagerUseCase : UseCase, IAccountManagerUseCase
{
    private readonly IEventDispatcherService eventDispatcherService;


    public AccountManagerUseCase(IEventDispatcherService _eventDispatcherService)
    {
        eventDispatcherService = _eventDispatcherService;
    }

    public void SignIn(string mail, string pass)
    {
        eventDispatcherService.Dispatch(new SignInEvent(mail,pass));
    
    }
    public void CreateAccount(string mail, string pass)
    {
        eventDispatcherService.Dispatch(new CreateAccountEvent(mail,pass));

    }    
}