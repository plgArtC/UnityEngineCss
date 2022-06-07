using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirebaseLoginScript : MonoBehaviour
{
    public FirebaseAuth fbAuth;
    public TMP_InputField tmpLogin;
    public TMP_InputField tmpPassword;
    public TextMeshProUGUI debugLog;

    public string userresult;

    private string loginId;
    private string password;


    // Start is called before the first frame update
    void Start()
    {
        fbAuth = FirebaseAuth.DefaultInstance;      
    }
    
    // Misafir giriþ, kullanýcý oluþturmadan
    public void singInAnonymously()
    {
        fbAuth.SignInAnonymouslyAsync().ContinueWith(task => {
        FirebaseUser newUserAnonymously1 = task.Result;
        debugLog.text = newUserAnonymously1.DisplayName;
        Debug.Log(newUserAnonymously1.DisplayName);
        });
    }

    //Kayýt Oluþturma
    public void singIn()
    {
        loginId = tmpLogin.text;
        password = tmpPassword.text;
        fbAuth.CreateUserWithEmailAndPasswordAsync(loginId, password).ContinueWith(task =>
        {
            if (task.IsCanceled) { debugLog.text = " canceled ! "; Debug.Log("HATA 2 : " + loginId + "  " + password); return; }
            if (task.IsFaulted) { debugLog.text = " faulted ! "; Debug.Log("HATA 1 : "+ loginId +"  "+ password); return; }
            if (task.IsCompleted) {
                debugLog.text = " LOGIN SUCCESS ! ";
                Debug.Log("BASARILI ");
                FirebaseUser newUser = task.Result;
            }
           
            
        });
    }
 

    //Giriþ Ýþlemi
    public void logIn()
    {
        loginId = tmpLogin.text;
        password = tmpPassword.text;
        fbAuth.SignInWithEmailAndPasswordAsync(loginId, password).ContinueWith(task =>
        {
            FirebaseUser loginUser = task.Result;
            print(loginUser.UserId);
            userresult = " " + loginUser.UserId;
            debugLog.text = userresult;

        });
    }

   //Þifre yenilemek için Mail gönderme
    public void resetPassword()
    {
        loginId = tmpLogin.text;
        fbAuth.SendPasswordResetEmailAsync(loginId).ContinueWith(task => 
        {
            if (task.IsCanceled) { print("Cancel"); return; }
            if(task.IsFaulted) { print("Fault !!! ");return; }
            if(task.IsCompleted) { print("Complete !!!! "); return; }
        });
    }
}
