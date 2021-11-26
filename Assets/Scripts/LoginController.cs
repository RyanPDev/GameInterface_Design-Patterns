using UniRx;

public class LoginController
{
    private readonly LoginViewModel loginPanelViewModel;
    private readonly ILoginUseCase loginUseCase;

    public LoginController(LoginViewModel _loginPanelViewModel, ILoginUseCase _loginUseCase)
    {
        loginPanelViewModel = _loginPanelViewModel;
        loginUseCase = _loginUseCase;

        loginPanelViewModel.LoginButtonPressed.Subscribe((_) =>
        {
            loginUseCase.Login();
        });
    }
}