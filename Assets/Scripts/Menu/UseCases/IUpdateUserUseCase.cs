public interface IUpdateUserUseCase
{
    void UpdateUsername(string userName);

    void UpdateAudio(bool audio);

    void UpdateNotifications(bool notifications);
}