using Firebase.Firestore;
using Firebase.Extensions;

public class FirebaseLoginService : IFirebaseLoginService
{
    readonly IEventDispatcherService eventDispatcher;
   // private bool firstLogin;

    public FirebaseLoginService(IEventDispatcherService _eventDispatcher)
    {
        eventDispatcher = _eventDispatcher;
    }

    //Authentifcation
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
                eventDispatcher.Dispatch(new UserInFirebase(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser != null));
                //eventDispatcher.Dispatch(new UserInFirebase(false));
                //firstLogin = !(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser != null);
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                return;
                //Firebase Unity SDK is not safe to use here.
            }
        });
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
            //if (firstLogin) SetData();
            SetDataIfUserExists();
            eventDispatcher.Dispatch(new LoginEvent(GetID()));
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
               if  (document.Id == Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId)
               {
                   return;
               }
           }
           SetData();
       });
   }

    //Database
    public void SetData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        var user = new User(GetID());
        DocumentReference docRef = db.Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);
    
        docRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            //if (task.IsCompleted)
            //    LoadData();
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
}