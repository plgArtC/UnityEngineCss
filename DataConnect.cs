using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

//baseDataTableClass
public class Users : MonoBehaviour
{
    public string userID;
    public int userLevel;
    public bool userLoginStatus;

    public Users(int userLevel, string userID,  bool userLoginStatus)
    {
        this.userID = userID;
        this.userLevel = userLevel;
        this.userLoginStatus = userLoginStatus;
    }
}





//task connect class
public class DataConnect : MonoBehaviour
{
    public DatabaseReference userReferance;
    Users userData;
    public FirebaseAuth fbAuth;




    // Start is called before the first frame update
    void Start()
    {
        fbAuth = FirebaseAuth.DefaultInstance;
        //singInAnonymously();
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if(dependencyStatus == DependencyStatus.Available) {
                Debug.Log("Connect Success ! ");
                userReferance = FirebaseDatabase.DefaultInstance.GetReference("gameUsers");
                SaveData("emre", 5, true);          
            
            }         
       
        
        });
        
    }

    void SaveData(string userName, int userLvl, bool isUserLogin)
    {
        userData = new Users(userLvl, userName,  isUserLogin);
        string converJson =JsonUtility.ToJson(userData);
        string userID = userReferance.Push().Key;
        userReferance.Child(userID).SetRawJsonValueAsync(converJson);
    }


    /// <summary>
    /// ////////////
    /// </summary>
    public void singInAnonymously()
    {
        fbAuth.SignInAnonymouslyAsync().ContinueWith(task => {
            FirebaseUser newUserAnonymously1 = task.Result;
            //debugLog.text = newUserAnonymously1.DisplayName;
            Debug.Log(newUserAnonymously1.DisplayName);
        });
    }
}
