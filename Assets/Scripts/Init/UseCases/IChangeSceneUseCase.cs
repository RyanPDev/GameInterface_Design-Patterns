public interface IChangeSceneUseCase
{
    void ChangeSceneToMenu(LoginEvent loginEvent);
    void ChangeScene(int scene);
}