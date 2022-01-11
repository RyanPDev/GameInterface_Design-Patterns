using UniRx;

class SignInController : Controller
{
    public SignInController(SignInPanelViewModel viewModel, AccountManagerUseCase accountManagerUseCase)
    {
        viewModel.OnBackButtonPressed.Subscribe((_) =>
        {
            viewModel.IsVisible.Value = false;
        }).AddTo(_disposables);

        viewModel.OnSignInButtonPressed.Subscribe((taskText) =>
        {
            if (viewModel.signInAction.Value)
                accountManagerUseCase.SignIn(taskText.mail, taskText.password);
            else
                accountManagerUseCase.CreateAccount(taskText.mail, taskText.password);
        }).AddTo(_disposables);
    }
}
