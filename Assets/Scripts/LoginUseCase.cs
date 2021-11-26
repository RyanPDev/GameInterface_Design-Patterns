using UnityEngine;
public class LoginUseCase : ILoginUseCase
{
    private readonly IFirebaseLoginService firebaseLoginService;
    private readonly IEventDispatcherService eventDispatcherService;

    public LoginUseCase(IFirebaseLoginService _firebaseLoginService, IEventDispatcherService _eventDispatcherService)
    {
        firebaseLoginService = _firebaseLoginService;
        eventDispatcherService = _eventDispatcherService;
        eventDispatcherService.Subscribe<FirebaseConnection>(AlreadyExists);
    }

    public void Login()
    {
        firebaseLoginService.Login();

        eventDispatcherService.Dispatch(new LogEvent(firebaseLoginService.GetID()));
    }

    public void AlreadyExists(FirebaseConnection firebase)
    {
        Debug.Log("Entra");
        if (firebase.isConnected)
        {
            {
                Debug.Log("Entra tambien");
                eventDispatcherService.Dispatch(new LogEvent(firebaseLoginService.GetID()));
                //SetUserID();
                Debug.Log("UserExists");
            }
        }
    }

    public void SetUserID()
    {
        Debug.Log("SetUserID");
        Debug.Log("logevent");
        //var logEvent = new LogEvent(firebaseLoginService.GetID());
        //eventDispatcherService.Dispatch(logEvent);
        Debug.Log("dispatcher");
    }
}