public class UserDto : Dto
{
    public string Name;

    public UserDto()
    {
    }

    public UserDto(string name)
    {
        Name = name;
    }
}