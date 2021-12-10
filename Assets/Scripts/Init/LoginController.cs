using UniRx;

public class LoginController : Controller
{
    private readonly LoginViewModel loginPanelViewModel;
    private readonly ILoginUseCase loginUseCase;

    public LoginController(LoginViewModel _loginPanelViewModel, ILoginUseCase _loginUseCase)
    {
        loginPanelViewModel = _loginPanelViewModel;
        loginUseCase = _loginUseCase;

        //loginPanelViewModel.isVisible.Value = true;

        loginPanelViewModel.LoginButtonPressed.Subscribe((_) =>
        {
            loginUseCase.Login();
        }).AddTo(_disposables);
    }
}