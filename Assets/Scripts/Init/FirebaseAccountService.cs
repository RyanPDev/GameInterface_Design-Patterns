using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;
using UnityEngine;
using System;

public class FirebaseAccountService : Service, IFirebaseAccountService
{
    readonly IEventDispatcherService eventDispatcher;
    readonly UserRepository userRepository;
    public FirebaseAccountService(IEventDispatcherService _eventDispatcher, UserRepository _userRepository)
    {
        userRepository = _userRepository;
        eventDispatcher = _eventDispatcher;
        eventDispatcher.Subscribe<SignInEvent>(SignIn);
        eventDispatcher.Subscribe<CreateAccountEvent>(Create);
    }

    public override void Dispose()
    {
        base.Dispose();
        eventDispatcher.Unsubscribe<SignInEvent>(SignIn);
        eventDispatcher.Unsubscribe<CreateAccountEvent>(Create);
    }

    public void SignIn(SignInEvent user)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInWithEmailAndPasswordAsync(user.mail, user.password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                eventDispatcher.Dispatch(new SignInSuccessfully(false, "Invalid mail or password"));
                return;
            }
            eventDispatcher.Dispatch(new SignInSuccessfully(true));
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat(newUser.Email);
            PlayerPrefs.SetString("UserEmail", user.mail);
            PlayerPrefs.SetString("UserPassword", user.password);
            PlayerPrefs.Save();
            LoadUserName();
        });
    }

    public void LoadUserName()
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
                    userRepository.SetLocalUser(new UserEntity(user.Name));
                    break;
                }
            }
        });
    }

    public void Create(CreateAccountEvent newUser)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.CreateUserWithEmailAndPasswordAsync(newUser.mail, newUser.password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                eventDispatcher.Dispatch(new CreateAccountSuccessfully(false, task.Exception.ToString()));
                return;
            }

            SetData(new User(userRepository.GetLocalUser().Name));
            eventDispatcher.Dispatch(new CreateAccountSuccessfully(true));
        });
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