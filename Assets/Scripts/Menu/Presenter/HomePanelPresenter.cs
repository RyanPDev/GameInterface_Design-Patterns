class HomePanelPresenter : Presenter
{
    private readonly IEventDispatcherService eventDispatcherService;
    private readonly HomePanelViewModel viewModel;

    public HomePanelPresenter(HomePanelViewModel _viewModel, IEventDispatcherService _eventDispatcherService, UserEntity user)
    {
        viewModel = _viewModel;

        eventDispatcherService = _eventDispatcherService;
        UpdateName(user);
        eventDispatcherService.Subscribe<UserEntity>(UpdateName);
    }

    public override void Dispose()
    {
        base.Dispose();
        eventDispatcherService.Unsubscribe<UserEntity>(UpdateName);
    }

    public void UpdateName(UserEntity user)
    {   
        viewModel.Username.Value = user.Name;
    }
}