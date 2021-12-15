public class UserDto : Dto
{
    public string Name;
    public bool Audio;
    public bool Notifications;

    public UserDto() { }

    public UserDto(string name, bool audio, bool notifications)
    {
        Name = name;
        Audio = audio;
        Notifications = notifications;  
    }
}