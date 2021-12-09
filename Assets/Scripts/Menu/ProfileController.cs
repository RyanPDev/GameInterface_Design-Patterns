using UniRx;

class ProfileController : Controller
{
    private readonly IEventDispatcherService _eventDispatcherService;
    public ProfileController(ProfileViewModel viewModel, UpdateUsernameUseCase updateUserUseCase)
    {
        viewModel.OnBackButtonPressed.Subscribe((_) => 
        {
            viewModel.IsVisible.Value = false;
        }).AddTo(_disposables);

        viewModel.OnSaveButtonPressed.Subscribe((taskText) =>
        {
            updateUserUseCase.UpdateUsername(taskText);
            viewModel.IsVisible.Value = false;
        }).AddTo(_disposables);
    }
}

