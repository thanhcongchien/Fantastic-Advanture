using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //public InterstitialAdFacebook fbAds;
    //public AdMobBannerInterstitial GaAds;
    //public AdMobRectBanner RectBanner;
    //public AdMobBanner Banner;

    public static Timer timer;

    //thay doi game time => thay doi timerbarprocess
    public float GameTime = 90;
    private float maxTime = 90;
    public float AddGameTime { set => GameTime = GameTime + value >= maxTime ? maxTime : GameTime + value; }
    private Slider sliderTime;

    public Texture2D timebarTexture;

    public TimerUpdate update;

    public GameObject NoSelect;

    public GameObject PauseUI;

    public GameObject WinUI;

    public GameObject LoseUI;

    public GameObject Nomove;

    private float _time;

    private const int ClassicBaseScore = 5000;

    private int ClassicTargetScore;

    public int ScoreStack = 0;

    private bool startplus;

    public bool isAds;

    public bool isreq;

    public enum GameState
    {
        PLAYING = 0,
        PAUSE = 1,
        WIN = 2,
        LOST = 3,
    }

    void Awake()
    {
        timer = this;
    }
    void Start()
    {
        ServiceLocator.Set(this);
        sliderTime = ((UIViewActionPhase)ServiceLocator.GetUIViewManager.GetBaseUiView<UIViewActionPhase>()).SlideTime;
        _time = GameTime;
        sliderTime.value = 0;
        StartCoroutine(AdsCd());
    }
    private void OnDestroy()
    {
        ServiceLocator.Unset<Timer>();
    }
    public void TimeTick(bool b)
    {
        if (b && PLayerInfo.MODE == 1)
        {
            update.enabled = true;
        }
        else
        {
            update.enabled = false;
        }

    }
    public void DisableTimeUpdate()
    {
        update.enabled = false;
    }
    public void EnableTimeUpdate()
    {
        update.enabled = true;
    }
    void timebarprocess(float time)
    {
        float fillamount = time / _time;
        sliderTime.value = fillamount;
    }

    public void ScoreBarProcess(int score)
    {
        ScoreStack += score;
        if (!startplus)
        {
            startplus = true;
            StartCoroutine(IEScoreBarProcess());
        }


    }
    IEnumerator IEScoreBarProcess()
    {
        while (ScoreStack > 0 && GameController.action.GameState == (int)GameState.PLAYING)
        {
            ScoreStack -= 10;
            if (PLayerInfo.Info.Score + 10 < 5000 * PLayerInfo.MapPlayer.Level)
            { PLayerInfo.Info.Score += 10; }
            else
            {
                PLayerInfo.Info.Score = 5000 * PLayerInfo.MapPlayer.Level;
                break;
            }
            float fillamount = PLayerInfo.Info.Score / (5000f * PLayerInfo.MapPlayer.Level);
            sliderTime.value = fillamount;
            yield return null;
        }

        startplus = false;
    }
    public void Tick()
    {
        if (GameTime > 0 && GameController.action.GameState == (int)GameState.PLAYING)
        {
            GameTime -= Time.deltaTime;
            timebarprocess(GameTime);
        }
        else if (GameController.action.GameState == (int)GameState.PLAYING)
        {
            GameController.action.GameState = (int)GameState.LOST;
            GameTime = 0;
            Lost();
            update.enabled = false;
        }
    }

    public void Win()
    {
        PlayerPrefs.SetInt("LevelShowRate", PlayerPrefs.GetInt("LevelShowRate") + 100);
        GameController.action.GameState = (int)GameState.WIN;
        NoSelect.SetActive(true);
        StartCoroutine(IEWin());
        Debug.Log("WIN");
    }
    public void Lost()
    {
        PlayerPrefs.SetInt("LevelShowRate", PlayerPrefs.GetInt("LevelShowRate") + 100);
        GameController.action.GameState = (int)GameState.LOST;
        NoSelect.SetActive(true);
        EffectSpawner.effect.SetScore(PLayerInfo.Info.Score);
        StartCoroutine(DisableAll());
        SoundController.Sound.Lose();
        showFullAds();
        Debug.Log("LOSE");
    }
    public void Pause()
    {
        SoundController.Sound.Click();
        if (GameController.action.GameState == (int)GameState.PLAYING)
        {
            GameController.action.GameState = (int)GameState.PAUSE;
            NoSelect.SetActive(true);
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }

    }
    public void Resume()
    {
        SoundController.Sound.Click();
        if (GameController.action.GameState == (int)GameState.PAUSE)
        {
            GameController.action.GameState = (int)GameState.PLAYING;
            Time.timeScale = 1;
            NoSelect.SetActive(false);
            PauseUI.SetActive(false);
        }
    }
    public void Restart()
    {
        if (PLayerInfo.MODE == 1)
        {
            PLayerInfo.Info.Score = 0;
            ButtonActionController.Click.ArcadeScene(PLayerInfo.MapPlayer);
        }
        else
        {
            ButtonActionController.Click.ClassicScene(1);
        }
    }
    public void Home()
    {
        ButtonActionController.Click.SelectMap(PLayerInfo.MODE);
    }
    public void Next()
    {
        ButtonActionController.Click.SelectMap(1);
        if (PLayerInfo.MapPlayer.Level < 297)
        {
            CameraMovement.StarPointMoveIndex = PLayerInfo.MapPlayer.Level;
            PLayerInfo.CurrentLevel = PLayerInfo.MapPlayer.Level;
        }
        else
            CameraMovement.StarPointMoveIndex = -1;
    }

    public void Music(UnityEngine.UI.Button button)
    {
        ButtonActionController.Click.BMusic(button);
    }
    public void Sound(UnityEngine.UI.Button button)
    {

    }
    public void ClassicLvUp()
    {
        GameController.action.GameState = (int)GameState.WIN;
        NoSelect.SetActive(true);
        StartCoroutine(UpLevel());

    }
    IEnumerator DisableAll()
    {
        DisableJewel(false);
        yield return new WaitForSeconds(0.75f);
        //LoseUI.SetActive(true);
        ServiceLocator.GetUIViewManager.ShowUIView<UIViewFailLevel>();
    }
    IEnumerator IEWin()
    {
        DisableJewel(true);
        EffectSpawner.effect.StarWinEffect(GameController.action.JewelStar.gameObject.transform.position);
        SoundController.Sound.Win();
        GameController.action.JewelStar.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        //WinUI.SetActive(true);
        ServiceLocator.GetUIViewManager.ShowUIView<UIViewWinLevel>();
        showFullAds();
    }

    void showFullAds()
    {
        //if (isAds)
        //{
        //    if (!AdmobFB.load.isOnFb)
        //    {
        //        GaAds.ShowBanner();
        //    }
        //    else
        //    {
        //        fbAds.LoadInterstitial();
        //    }
        //    isAds = false;
        //    isreq = false;
        //}
    }

    IEnumerator UpLevel()
    {
        DisableJewel(true);
        showFullAds();
        yield return new WaitForSeconds(1f);
        ButtonActionController.Click.ClassicScene(PLayerInfo.MapPlayer.Level + 1);
    }

    public void DisableJewel(bool b)
    {
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                if (!b)
                {
                    if (JewelSpawner.spawn.JewelGribScript[x, y] != null)
                        JewelSpawner.spawn.JewelGribScript[x, y].JewelDisable();
                }
                else
                {
                    if (JewelSpawner.spawn.JewelGribScript[x, y] != null && JewelSpawner.spawn.JewelGribScript[x, y] != GameController.action.JewelStar)
                        JewelSpawner.spawn.JewelGribScript[x, y].JewelDisable();
                }
            }
        }
    }

    IEnumerator AdsCd()
    {
        while (true)
        {
            yield return new WaitForSeconds(119f);
            isAds = true;
        }
    }


}
