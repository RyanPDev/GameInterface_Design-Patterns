using UniRx;

public class LoginController
{
    LoginModelView _loginPanelViewModel;

    public LoginController(LoginModelView loginPanelViewModel)
    {
        _loginPanelViewModel = loginPanelViewModel;
        _loginPanelViewModel.IsVisible.Value = true;

        loginPanelViewModel.LoginButtonPressed.Subscribe((_) =>
        {
            loginPanelViewModel.IsVisible.Value = false;
            //_loginPanelViewModel.TextID.SetValueAndForceNotify(_textId);
        });
    }
}
