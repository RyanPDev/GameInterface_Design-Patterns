using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseLoginService : Service, IFirebaseLoginService
{
    readonly IEventDispatcherService eventDispatcher;

    public FirebaseLoginService(IEventDispatcherService _eventDispatcher)
    {
        eventDispatcher = _eventDispatcher;
        eventDispatcher.Subscribe<UserEntity>(UpdateData);
    }

    //Authentifcation
    public void InitAsync()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                var app = Firebase.FirebaseApp.DefaultInstance;
                eventDispatcher.Dispatch(new UserInFirebase(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser != null));
            }
            else
            {
                return;
            }
        });
    }

    public override void Dispose()
    {
        base.Dispose();
        eventDispatcher.Unsubscribe<UserEntity>(UpdateData);
    }

    public void Login()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            SetDataIfUserExists();
        });
    }

    public string GetID()
    {
        return Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    }

    public void SetDataIfUserExists()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        CollectionReference usersRef = db.Collection("users");

        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Id == Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId)
                {
                    LoadData();
                    return;
                }
            }
            InitUserData();
        });
    }

    public void UpdateData(UserEntity userEntity)
    {
        SetData(new User(userEntity.Name, userEntity.Audio, userEntity.Notifications));
    }

    //Database 
    public void InitUserData()
    {
        //Initial user info, audio and notificiations set both initially to true        
        SetData(new User(GetID(), true, false), true); //TODO---> Ask user at start for notifications
    }

    public void SetData(User user, bool saveInRepo = false)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);

        docRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && saveInRepo)
                LoadData();
        });
    }

    public void LoadData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        if (PlayerPrefs.HasKey("UserEmail"))
        {
            eventDispatcher.Dispatch(new SignInEvent(PlayerPrefs.GetString("UserEmail"), PlayerPrefs.GetString("UserPassword")));
        }

        CollectionReference usersRef = db.Collection("users");
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Id == Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId)
                {
                    // toda tu info
                    User user = document.ConvertTo<User>();

                    eventDispatcher.Dispatch(new UserInfo(user.Name, user.Audio, user.Notifications));
                    // Dispatch para cambiar de escena <--- DATOS DE USUARIO CARGADOS
                    eventDispatcher.Dispatch(new LoginEvent(user.Name));
                    break;
                }
            }
        });
    }
}