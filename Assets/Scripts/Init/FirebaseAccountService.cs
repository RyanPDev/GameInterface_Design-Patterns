using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;
using UnityEngine;
using System;


public class FirebaseAccountService : Service, IFirebaseAccountService
{
    readonly IEventDispatcherService eventDispatcher;
    public FirebaseAccountService(IEventDispatcherService _eventDispatcher)
    {
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
        auth.SignInWithEmailAndPasswordAsync(user.mail, user.password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
             //   Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
               
                eventDispatcher.Dispatch(new SignInSuccessfully(false,task.Exception.ToString()));
             //   Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            eventDispatcher.Dispatch(new SignInSuccessfully(true));
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat(newUser.Email , "  ", newUser.DisplayName);
            PlayerPrefs.SetString("UserEmail",user.mail);
            PlayerPrefs.SetString("UserPassword", user.password);

            PlayerPrefs.Save();
          
            // Debug.LogFormat("User signed in successfully: {0} ({1})",
              //newUser.DisplayName, newUser.UserId);
        });

    }
    public void Create(CreateAccountEvent newUser)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.CreateUserWithEmailAndPasswordAsync(newUser.mail, newUser.password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                eventDispatcher.Dispatch(new CreateAccountSuccessfully(false, task.Exception.ToString()));
                return;
            }
       
            eventDispatcher.Dispatch(new CreateAccountSuccessfully(true));
            // Firebase user has been created.
            //Firebase.Auth.FirebaseUser newUser = task.Result;
            
            //   Debug.LogFormat("Firebase user created successfully: {0} ({1})",
            //     newUser.DisplayName, newUser.UserId);
        });
    }
}

