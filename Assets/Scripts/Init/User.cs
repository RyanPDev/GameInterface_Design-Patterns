using Firebase.Firestore;

[FirestoreData]
public class User
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public bool Audio { get; set; }

    [FirestoreProperty]
    public bool Notifications { get; set; }

    public User() { }

    public User(string name, bool audio, bool notifications)
    {
        Name = name;
        Audio = audio;
        Notifications = notifications;
    }
}