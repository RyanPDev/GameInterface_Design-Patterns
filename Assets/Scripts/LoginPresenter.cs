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
    }
    private void OnLogID(LoginEvent data)
    {
        viewModel.IsVisible.Value = false;
        viewModel.TextID.SetValueAndForceNotify("User ID: " + data.Text);
    }

    private void ButtonVisibility(UserInFirebase userExists)
    {  
        viewModel.IsVisible.Value = !userExists.existsInFirebase;
    }
}