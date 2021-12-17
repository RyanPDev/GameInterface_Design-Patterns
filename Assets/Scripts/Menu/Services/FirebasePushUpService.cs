public class FirebasePushUpService : Service
{
    readonly IEventDispatcherService eventDispatcher;

    public FirebasePushUpService(IEventDispatcherService _eventDispatcher)
    {
        eventDispatcher = _eventDispatcher;
        Firebase.Messaging.FirebaseMessaging.TokenRegistrationOnInitEnabled = false;
        eventDispatcher.Subscribe<NotificationsHandler>(Notifications);
    }

    public void Notifications(NotificationsHandler n)
    {
        if (n.isOn != Firebase.Messaging.FirebaseMessaging.TokenRegistrationOnInitEnabled)
        {
            Firebase.Messaging.FirebaseMessaging.TokenRegistrationOnInitEnabled = n.isOn;
            if (n.isOn)
            {
                Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
            }
            else
            {
                Firebase.Messaging.FirebaseMessaging.TokenReceived -= OnTokenReceived;
                Firebase.Messaging.FirebaseMessaging.MessageReceived -= OnMessageReceived;
            }
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        eventDispatcher.Unsubscribe<NotificationsHandler>(Notifications);
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }
}