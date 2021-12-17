public class UserInfo
{
    public string Name;
    public bool Audio;
    public bool Notifications;

    public UserInfo() { }

    public UserInfo(string name, bool audio, bool notifications)
    {
        Name = name;
        Audio = audio;
        Notifications = notifications;  
    }
}