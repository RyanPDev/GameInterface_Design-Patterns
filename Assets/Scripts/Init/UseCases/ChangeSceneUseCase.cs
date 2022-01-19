using UnityEngine.SceneManagement;

public class ChangeSceneUseCase : UseCase, IChangeSceneUseCase
{
    private readonly IEventDispatcherService eventDispatcherService;

    public ChangeSceneUseCase(IEventDispatcherService _eventDispatcherService)
    {
        eventDispatcherService = _eventDispatcherService;
        eventDispatcherService.Subscribe<LoginEvent>(ChangeSceneToMenu);
    }

    public void ChangeSceneToMenu(LoginEvent logged)
    {
        SceneManager.LoadScene(1);
    }
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public override void Dispose()
    {
        base.Dispose();
        eventDispatcherService.Unsubscribe<LoginEvent>(ChangeSceneToMenu);
    }
}