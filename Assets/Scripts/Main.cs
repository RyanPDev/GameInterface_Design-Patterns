using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;


public class Main : MonoBehaviour
{
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
                var app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                Debug.Log("Firebase ok");
                Login();

            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                //Firebase Unity SDK is not safe to use here.
                Debug.LogError("Firebase error");
            }
        });
    }

    void Login()
    {
        if (Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            Debug.Log("Entras");
            //Ya esta autentificado
            SetData();
            Debug.Log("Entras tambien");
            return;
        }

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
            Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            SetData();
        });
    }

    void SetData()
    {
        Debug.Log("SetData");
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        var user = new User("Ramis", 5);
        DocumentReference docRef = db.Collection("users").Document(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId);

        docRef.SetAsync(user).ContinueWithOnMainThread(task =>
        {
            Debug.Log("taskCompleted");
            if (task.IsCompleted)
            {
                LoadData();
                Debug.Log("Added data in the users collection.");
            }
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
                var user = document.ConvertTo<User>();

                Debug.Log(String.Format("Name: {0}", user.Name));
                Debug.Log(String.Format("Level: {0}", user.Level));
            }

            Debug.Log("Read all data from the users collection.");
        });
    }

    // Update is called once per frame
    void Update()
    {
    }
}


[FirestoreData]
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
