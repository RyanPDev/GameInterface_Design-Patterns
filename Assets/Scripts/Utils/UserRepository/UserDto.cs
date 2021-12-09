// [FirestoreData]
public class UserDto : Dto
{
    // [FirestoreDocumentId] 
    public string UserId { get; set; }
    // [FirestoreProperty]
    public string Name { get; set; }

    public UserDto()
    {
    }

    public UserDto(string userId, string name)
    {
        UserId = userId;
        Name = name;
    }
}