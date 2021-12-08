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

        //_eventDispatcherService.Subscribe<NewTaskCreatedEvent>(OnNewTaskCreated);
        //_eventDispatcherService.Subscribe<TaskDeletedEvent>(OnTaskDeleted);
    }

    public override void Dispose()
    {
        base.Dispose();
        //_eventDispatcherService.Unsubscribe<NewTaskCreatedEvent>(OnNewTaskCreated);
        //_eventDispatcherService.Unsubscribe<TaskDeletedEvent>(OnTaskDeleted);
    }
}