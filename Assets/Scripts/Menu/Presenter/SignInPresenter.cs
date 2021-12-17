class SignInPresenter : Presenter
{
    private readonly IEventDispatcherService eventDispatcherService;
    private readonly SignInPanelViewModel viewModel;

    public SignInPresenter(SignInPanelViewModel _viewModel, IEventDispatcherService _eventDispatcherService)
    {
        viewModel = _viewModel;

        eventDispatcherService = _eventDispatcherService;
        eventDispatcherService.Subscribe<SignInSuccessfully>(OnSigned);
    }

    public override void Dispose()
    {
        base.Dispose();
        eventDispatcherService.Unsubscribe<SignInSuccessfully>(OnSigned);
    }

    public void OnSigned(SignInSuccessfully e)
    {
        if (e.signInOk)
            viewModel.IsVisible.Value = false;
        else
            if (e.exception != null)
            viewModel.eText.Value = e.exception;
    }
}