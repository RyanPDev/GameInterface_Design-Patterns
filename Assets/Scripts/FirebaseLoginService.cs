using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseLoginService : IFirebaseLoginService
{
    readonly IEventDispatcherService eventDispatcher;

    public FirebaseLoginService(IEventDispatcherService _eventDispatcher)
    {
        eventDispatcher = _eventDispatcher;
    }

    public void Init()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.º
                var app = Firebase.FirebaseApp.DefaultInstance;
                //Check(connection);
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                return;
                //Firebase Unity SDK is not safe to use here.
            }
        });

        eventDispatcher.Dispatch(new FirebaseConnection(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser != null));
    }

    public void Login()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            SetData();
        });
    }

    public void SetData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        var user = new User("Palazon", 9);
        DocumentReference docRef = db.Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);

        docRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
                LoadData();
        });
    }

    public void LoadData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        CollectionReference usersRef = db.Collection("users");

        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var user = document.ConvertTo<User>();
            }
        });
    }

    public string GetID()
    {
        return Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    }
}