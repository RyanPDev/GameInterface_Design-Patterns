using UniRx;

class SignInController : Controller
{
    public SignInController(SignInPanelViewModel viewModel, AccountManagerUseCase ManageAccountUseCase)
    {
        viewModel.OnBackButtonPressed.Subscribe((_) =>
        {
            viewModel.IsVisible.Value = false;
        }).AddTo(_disposables);

        viewModel.OnSignInButtonPressed.Subscribe((taskText) =>
        {
           if(viewModel.signInAction.Value)
            {
                ManageAccountUseCase.SignIn(taskText.mail,taskText.password);
            }
            else
            {
                ManageAccountUseCase.CreateAccount(taskText.mail, taskText.password);
            }
            //viewModel.IsVisible.Value = false;
        }).AddTo(_disposables);
    }
}
