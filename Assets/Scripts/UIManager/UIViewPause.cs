using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIViewPause : BaseUIView
{
    [SerializeField] Button btnClose;
    [SerializeField] Button btnContinue;
    [SerializeField] Button btnGiveUp;
    [SerializeField] Slider sliderSound;
    [SerializeField] Slider sliderMusic;

    SoundController soundController;
    MusicController musicController;
    // Start is called before the first frame update
    private void Awake()
    {
        soundController = ServiceLocator.Get<SoundController>();
        musicController = ServiceLocator.Get<MusicController>();
    }
    void Start()
    {
        btnClose.onClick.AddListener(ClickClose);
        btnContinue.onClick.AddListener(() => { Close(); });
        btnGiveUp.onClick.AddListener(() => {
            ServiceLocator.GetUIViewManager.CloseAllUIView();
            ServiceLocator.Get<Timer>().Home();
        });


        sliderSound.onValueChanged.AddListener((value) =>
        {
            soundController.audiosource.volume = value;
        });
        sliderMusic.onValueChanged.AddListener((value) =>
        {
            musicController.audiosource.volume = value;
        });
    }
    private void ClickClose()
    {
        Close();
        PlayerPrefs.SetFloat("sound", ServiceLocator.Get<SoundController>().audiosource.volume);
        PlayerPrefs.SetFloat("music", ServiceLocator.Get<MusicController>().audiosource.volume);
    }
    private void OnEnable()
    {
        ChangeSliderSound();
    }
    private void ChangeSliderSound()
    {
        sliderSound.value = soundController.audiosource.volume;
        sliderMusic.value = musicController.audiosource.volume;
    }
}
