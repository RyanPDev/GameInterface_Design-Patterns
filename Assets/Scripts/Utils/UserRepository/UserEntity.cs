public class UserEntity
{
    public string Name { get; private set; }
    public bool Audio { get; private set; }
    public bool Notifications { get; private set; }

    public UserEntity(string name, bool audio, bool notifications)
    {
        Name = name;
        Audio = audio;
        Notifications = notifications;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void UpdateData(string name, bool audio, bool notifications)
    {
        Name = name;
        Audio = audio;
        Notifications = notifications;
    }
}