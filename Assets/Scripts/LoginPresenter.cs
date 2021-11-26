using UnityEngine;
public class LoginPresenter
{
    private readonly IEventDispatcherService eventDispatcherService;
    private readonly ILoginUseCase loginUseCase;
    private readonly LoginViewModel viewModel;

    public LoginPresenter(LoginViewModel _viewModel, ILoginUseCase _loginUseCase, IEventDispatcherService _eventDispatcherService)
    {
        viewModel = _viewModel;
        eventDispatcherService = _eventDispatcherService;
        loginUseCase = _loginUseCase;

        eventDispatcherService.Subscribe<LogEvent>(OnLogID);
    }

    private void OnLogID(LogEvent data)
    {
        viewModel.IsVisible.Value = false;
        viewModel.TextID.SetValueAndForceNotify("User ID: " + data.Text);
    }
}