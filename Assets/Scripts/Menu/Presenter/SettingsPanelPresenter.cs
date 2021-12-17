class SettingsPanelPresenter : Presenter
{
    private readonly IEventDispatcherService eventDispatcherService;
    private readonly SettingsPanelViewModel viewModel;

    public SettingsPanelPresenter(SettingsPanelViewModel _viewModel, IEventDispatcherService _eventDispatcherService)
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
        {
            viewModel.IsLoginVisible.Value = false;
            viewModel.IsCreateVisible.Value = false;
        }
    }
}
