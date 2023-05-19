using UnityEngine;
//using AudienceNetwork;

public class AdmobFB : MonoBehaviour
{

    public static AdmobFB load;     // instance of this class
    public static string BnId ;
    public static string ItId ;

    //public AdSize CurAdSize = AdSize.BANNER_HEIGHT_90;
    public bool Isactive = false;
    public bool isOnFb = false;
    public static int SpecialNum = 1;
    //public AdMobRectBanner RectBanner;

    void Awake()
    {
        //BnId = LoadWaiting.Id1;
        //ItId = LoadWaiting.Id2;

        //if (load == null)
        //{
        //    DontDestroyOnLoad(gameObject);
        //    load = this;
        //}
        //else if (load != this)
        //{
        //    Destroy(gameObject);
        //}

        //if (!Isactive)
        //{
        //    bool isActive = PlayerPrefs.GetInt("MaxLevel") >= SpecialNum;

        //    if (isActive)
        //    {
        //        GoogleMobileAdSettings.Instance.SetData(GoogleAnalyticsV3.getInstance());
        //    }
        //}

        //if (!GoogleMobileAd.IsInited)
        //{
        //    GoogleMobileAd.Init();
        //}

        //if (!AdmobFB.load.isOnFb)
        //{
        //    RectBanner.ShowRectBanner();
        //}
    }
}
