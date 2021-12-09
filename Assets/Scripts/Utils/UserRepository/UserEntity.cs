public class UserEntity
{
    public string Name { get; private set; }

    public UserEntity( string name)
    {
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;

    }
}