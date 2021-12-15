public class LoginPresenter : Presenter
{
    private readonly IEventDispatcherService eventDispatcherService;
    private readonly LoginViewModel viewModel;

    public LoginPresenter(LoginViewModel _viewModel, IEventDispatcherService _eventDispatcherService)
    {
        viewModel = _viewModel;
        eventDispatcherService = _eventDispatcherService;

        eventDispatcherService.Subscribe<LoginEvent>(OnLogID);
        eventDispatcherService.Subscribe<UserInFirebase>(ButtonVisibility);
    }
    public override void Dispose()
    {
        base.Dispose();
        eventDispatcherService.Unsubscribe<LoginEvent>(OnLogID);
        eventDispatcherService.Unsubscribe<UserInFirebase>(ButtonVisibility);
    }
    private void OnLogID(LoginEvent data)
    {
        viewModel.IsLogged.Value = true;
        viewModel.isVisible.Value = false;
    }

    private void ButtonVisibility(UserInFirebase userExists)
    {
        if (userExists.existsInFirebase)
        {
            viewModel.isVisible.Value = false;
        }
    }
}