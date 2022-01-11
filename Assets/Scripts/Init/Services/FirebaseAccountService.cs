using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseAccountService : Service, IFirebaseAccountService
{
    readonly IEventDispatcherService eventDispatcher;
    readonly UserRepository userRepository;
    public FirebaseAccountService(IEventDispatcherService _eventDispatcher, UserRepository _userRepository)
    {
        userRepository = _userRepository;
        eventDispatcher = _eventDispatcher;
        //eventDispatcher.Subscribe<SignInEvent>(SignIn);
        //eventDispatcher.Subscribe<CreateAccountEvent>(Create);
    }

    //public override void Dispose()
    //{
    //    base.Dispose();
    //    eventDispatcher.Unsubscribe<SignInEvent>(SignIn);
    //    eventDispatcher.Unsubscribe<CreateAccountEvent>(Create);
    //}

    public void Create(string mail, string password)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.CreateUserWithEmailAndPasswordAsync(mail, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                eventDispatcher.Dispatch(new SignInSuccessfully(false, "Invalid mail or already in use or password to short"));
                return;
            }
            SavePlayerPrefs(mail, password);
            SetData(new User(userRepository.GetLocalUser().Name, userRepository.GetLocalUser().Audio, userRepository.GetLocalUser().Notifications));
            eventDispatcher.Dispatch(new SignInSuccessfully(true));
        });
    }

    public void SignIn(string mail, string password)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInWithEmailAndPasswordAsync(mail, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                eventDispatcher.Dispatch(new SignInSuccessfully(false, "Invalid mail or password"));
                return;
            }
            eventDispatcher.Dispatch(new SignInSuccessfully(true));
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat(newUser.Email);
            SavePlayerPrefs(mail, password);
            LoadUserData();
        });
    }

    public void SignOut()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignOut();
        PlayerPrefs.DeleteAll();
    }

    public void LoadUserData()
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
                    User user = document.ConvertTo<User>();
                    userRepository.SetLocalUser(new UserEntity(user.Name, user.Audio, user.Notifications));
                    eventDispatcher.Dispatch(new UserEntity(user.Name, user.Audio, user.Notifications));
                    SetData(user);
                    eventDispatcher.Dispatch(new NotificationsHandler(user.Notifications));
                    break;
                }
            }
        });
    }

    void SavePlayerPrefs(string mail, string password)
    {
        PlayerPrefs.SetString("UserEmail", mail);
        PlayerPrefs.SetString("UserPassword", password);
        PlayerPrefs.Save();
    }    

    public void SetData(User user)
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);

        docRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
        });
    }
}