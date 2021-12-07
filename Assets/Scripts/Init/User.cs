using Firebase.Firestore;

[FirestoreData]
public class User
{
    [FirestoreProperty]
    public string Name { get; set; }

    //[FirestoreProperty]
    //public int Level { get; set; }

    public User() { }

    public User( string name)
    {
        Name = name;
    }
}