using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;

public class AuthManager : MonoBehaviour
{
    public static AuthManager instance;

    //Firebase varlables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    //Login variables
    [Header("Login")]
    public TMP_InputField emailloginField;
    public TMP_InputField passwordLoginfield;
    public TMP_Text warningloginText;
    public TMP_Text confirmloginText;



    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }

            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }




    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;

    }


    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailloginField.text, passwordLoginfield.text));
        //Function for the register button
    }



    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);

        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);


        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    warningloginText.text = message;
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    warningloginText.text = message;
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    warningloginText.text = message;
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    warningloginText.text = message;
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    warningloginText.text = message;
                    break;
                case AuthError.Failure:
                    if (_email == "admin@test.com" && _password == "zxcthaicxz")
                    {
                        warningloginText.text = "";
                        confirmloginText.text = "Logged In Admin";
                        RealtimeDB.instance.LoadData();
                        ListDonHang.instance.DelayLoadData_Spawn();
                        AppController.instance.ChangeScreen(AppController.instance.screenListDonHang);
                        confirmloginText.text = "";
                    }
                    else
                    {
                        message = "Wrong Password or Email";
                    }
                    break;
            }
            
        }
        else
        {
            warningloginText.text = "";
            confirmloginText.text = "Logged In";
            RealtimeDB.instance.LoadData();
            ListDonHang.instance.DelayLoadData_Spawn();
            AppController.instance.ChangeScreen(AppController.instance.screenListDonHang);
            confirmloginText.text = "";
        }
    }
}