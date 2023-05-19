using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Facebook.Unity;
public class ButtonActionController : MonoBehaviour
{

    public static ButtonActionController Click;     // instance of ButtonActionController

    public Sprite[] ButtonSprite;                   //sprite array of buttons
    void Awake()
    {
        if (Click == null)
        {
            DontDestroyOnLoad(gameObject);
            Click = this;
        }
        else if (Click != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// When select classic mode
    /// </summary>
    /// <param name="level">number of level</param>
    public void ClassicScene(int level)
    {
        SoundController.Sound.Click();
        Time.timeScale = 1;
        PLayerInfo.MODE = 0;
        PLayerInfo.MapPlayer = new Player();
        PLayerInfo.MapPlayer.Level = level;
        PLayerInfo.MapPlayer.HightScore = level;
        PLayerInfo.MapPlayer.HightScore = PlayerPrefs.GetInt(PLayerInfo.KEY_CLASSIC_HISCORE, 0);
        AddressableManager.LoadSingleScene(Definition.SceneDefinition.ADDRESSABLE_PLAY_SCENE);
    }


    /// <summary>
    /// When select arcade mode
    /// </summary>
    /// <param name="player">info of level to play</param>
    public void ArcadeScene(Player player)
    {
        SoundController.Sound.Click();
        Time.timeScale = 1;
        PLayerInfo.MODE = 1;
        PLayerInfo.MapPlayer = player;
        StartCoroutine(GotoScreen(Definition.SceneDefinition.ADDRESSABLE_PLAY_SCENE));
    }


    public void SelectMap(int mode)
    {
        SoundController.Sound.Click();
        if (mode == 1)
            AddressableManager.LoadSingleScene(Definition.SceneDefinition.ADDRESSABLE_MAP_SCENE);
        else
            HomeScene();

        CameraMovement.StarPointMoveIndex = -1;
    }

    /// <summary>
    /// Go to a scene with name
    /// </summary>
    /// <param name="screen">name of the scene to direction</param>
    IEnumerator GotoScreen(string screen)
    {
        yield return new WaitForSeconds(0);
        //Application.LoadLevel(screen);
        AddressableManager.LoadSingleScene(screen);
    }

    public void HomeScene()
    {
        ServiceLocator.GetUIViewManager.CloseAllUIView();
        SoundController.Sound.Click();
        Time.timeScale = 1;
        //Application.LoadLevel("HomeScene");
        AddressableManager.LoadSingleScene(Definition.SceneDefinition.ADDRESSABLE_HOME_SCENE);
    }

    /// <summary>
    /// Set and change state of music
    /// </summary>
    /// <param name="button">Image button</param>
    public void BMusic(UnityEngine.UI.Button button)
    {

        if (PlayerPrefs.GetInt("MUSIC", 0) != 1)
        {
            PlayerPrefs.SetInt("MUSIC", 1); // music off
        }
        else
        {
            PlayerPrefs.SetInt("MUSIC", 0); // music on
        }

    }
    /// <summary>
    /// Set and change state of sound background
    /// </summary>
    /// <param name="button">Image button</param>
    public void BSound(UnityEngine.UI.Image button)
    {
        if (PlayerPrefs.GetInt("SOUND", 0) != 1)
        {
            PlayerPrefs.SetInt("SOUND", 1);
            button.overrideSprite = ButtonSprite[3];
        }
        else
        {
            PlayerPrefs.SetInt("SOUND", 0);
            button.overrideSprite = ButtonSprite[2];
        }
    }

    public void OnShareClick()
    {
        //if(!FB.IsInitialized)
        //{
        //    FB.Init(OnInitComplete);
        //}
        //else if (FB.IsLoggedIn)
        //{
        //    FacebookController.load.Share();
        //}
        //else
        //{
        //    FacebookController.load.Login();
        //}

    }

    void OnInitComplete()
    {
        //if (FB.IsInitialized && FB.IsLoggedIn)
        //{
        //    FacebookController.load.Share();
        //}
        //else
        //{
        //    FacebookController.load.Login();
        //}
    }

    public void OnCloseDialog()
    {

    }

}
