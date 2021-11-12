using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;


public class MainV2 : MonoBehaviour
{
    private Firebase.FirebaseApp app;

    // Start is called before the first frame update
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                Debug.Log("Firebase ok");
                Login();

            }
            else
            {
                Debug.LogError("Firebase error");
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    void Login()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            SetData();
        });
    }

    void SetData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        // DocumentReference docRef = db.Collection("users").Document("alovelace");
        DocumentReference docRef = db.Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);
        Dictionary<string, object> user = new Dictionary<string, object>
{
        { "First", "Ada" },
        { "Last", "Lovelace" },
        { "Born", 1815 },
};
        //var user = new User("Dani", 1);
        docRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
                Debug.Log("Added data to the alovelace document in the users collection.");
            else
                Debug.LogError(task.Exception);

        });
    }

    void LoadData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        CollectionReference usersRef = db.Collection("users");
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Debug.Log(String.Format("User: {0}", document.Id));
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                Debug.Log(String.Format("First: {0}", documentDictionary["First"]));
                if (documentDictionary.ContainsKey("Middle"))
                {
                    Debug.Log(String.Format("Middle: {0}", documentDictionary["Middle"]));
                }

                Debug.Log(String.Format("Last: {0}", documentDictionary["Last"]));
                Debug.Log(String.Format("Born: {0}", documentDictionary["Born"]));
            }

            Debug.Log("Read all data from the users collection.");
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class User
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public int Level { get; set; }

    public User()
    {
    }

    public User(string name, int level)
    {
        Name = name;
        Level = level;
    }
}
