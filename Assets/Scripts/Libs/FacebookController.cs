using UnityEngine;
using System.Collections.Generic;
//using Facebook.Unity;
using System;

public class FacebookController : MonoBehaviour {

    public static FacebookController load;
    public string shareLink = "https://google.com.vn/";
    public string shareTitle = "Link Title";
    public string shareCaption = "This is caption";
    public string shareDescription = "Link Description";
    public string shareImage = "http://i.imgur.com/j4M7vCO.jpg";

    void Awake()
    {
        if (load == null)
        {
            // Makes the object target not be destroyed automatically when loading a new scene
            DontDestroyOnLoad(gameObject);
            load = this;
        }

        //FB.Init();
    }

    // Use this for initialization
    void Start () {
	
	}
	

    public void Login()
    {
        //FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, OnCompleteLogin);
    }

    //void OnCompleteLogin(IResult result)
    //{
    //    if (FB.IsInitialized && FB.IsLoggedIn)
    //    {
    //        Share();
    //    }
    //}

    public void Share()
    {
        int Point = PLayerInfo.Info.Score;
        int Level = PLayerInfo.MapPlayer.Level;

        try
        {
            //FB.FeedShare("", new Uri(shareLink), string.Format(shareTitle, Level), string.Format(shareCaption, Point), string.Format(shareDescription, Point), new Uri(shareImage), callback: this.HandleResult);
        }
        catch
        {
            //FB.FeedShare("", new Uri(shareLink), string.Format(shareTitle, Level), string.Format(shareCaption, Point), shareDescription, new Uri(shareImage), callback: this.HandleResult);
        }
    }


    //void HandleResult(IResult result)
    //{

    //}
}
