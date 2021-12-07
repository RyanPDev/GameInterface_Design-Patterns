using UnityEngine.SceneManagement;

public class ChangeSceneUseCase : UseCase, IChangeSceneUseCase
{
    private readonly IEventDispatcherService eventDispatcherService;

    public ChangeSceneUseCase(IEventDispatcherService _eventDispatcherService)
    {
        eventDispatcherService = _eventDispatcherService;
        eventDispatcherService.Subscribe<LoginEvent>(ChangeScene);
    }

    public void ChangeScene(LoginEvent logged)
    {
        SceneManager.LoadScene(1);
    }

    public override void Dispose()
    {
        base.Dispose();
        eventDispatcherService.Unsubscribe<LoginEvent>(ChangeScene);
    }
}