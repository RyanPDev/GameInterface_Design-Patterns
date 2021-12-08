using UniRx;

class ProfileController : Controller
    {
    private readonly IEventDispatcherService _eventDispatcherService;
   // public ProfileController(ProfileViewModel viewModel,
   //        ICreateTaskUseCase createTaskUseCase)
   // {
   //     taskPanelViewModel.OnDeleteButtonPressed.Subscribe(
   //             (_) => { taskPanelViewModel.IsVisible.Value = false; }
   //         )
   //         .AddTo(_disposables);
   //
   //     taskPanelViewModel.OnAddButtonPressed.Subscribe(
   //             (taskText) =>
   //             {
   //                 createTaskUseCase.Create(taskText);
   //                 taskPanelViewModel.IsVisible.Value = false;
   //             }
   //         )
   //         .AddTo(_disposables);
   // }
}

