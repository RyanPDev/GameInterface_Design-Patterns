using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;
using UnityEngine;

public class RealTimeDatabaseService : IRealTimeDatabaseService
{
    IEventDispatcherService eventDispatcher;

    public RealTimeDatabaseService(IEventDispatcherService _eventDispatcherService)
    {
        eventDispatcher = _eventDispatcherService;
    }

    public void SetData(int score)
    {
        var reference = FirebaseDatabase.DefaultInstance.RootReference;

        var jsonValue = JsonUtility.ToJson(new ScoreEntry(score));

        reference.Child("scores").Child(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .SetRawJsonValueAsync(jsonValue).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Data set");
                }
            });
    }

    public async Task<int> GetData()
    {
        var aux = await FirebaseDatabase.DefaultInstance.GetReference("scores/{UserId}").GetValueAsync();

        if (aux != null)
        {
            Debug.Log("Data get");
            foreach (var dataSnapshot in aux.Children)
            {
                foreach (var child in dataSnapshot.Children)
                {
                    return (int)child.Value;
                }
            }
        }

        return 0;
    }
}