public class LoginPresenter : Presenter
{
    private readonly IEventDispatcherService eventDispatcherService;
    private readonly LoginViewModel viewModel;

    public LoginPresenter(LoginViewModel _viewModel, IEventDispatcherService _eventDispatcherService)
    {
        viewModel = _viewModel;
        eventDispatcherService = _eventDispatcherService;
    }
    public override void Dispose()
    {
        base.Dispose();
       
    }
  
}