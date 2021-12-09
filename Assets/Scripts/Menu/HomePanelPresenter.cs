using System.Linq;
using UniRx;
class HomePanelPresenter : Presenter
{
    private readonly IEventDispatcherService eventDispatcherService;
    private readonly HomePanelViewModel viewModel;

    public HomePanelPresenter(HomePanelViewModel _viewModel, IEventDispatcherService _eventDispatcherService)
    {
        viewModel = _viewModel;

        eventDispatcherService = _eventDispatcherService;

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