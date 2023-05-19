using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIViewSetting : BaseUIView
{
    [SerializeField] Button btnClose;
    [SerializeField] TextMeshProUGUI txtUserName;
    [SerializeField] TextMeshProUGUI txtLevel;
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

        if (soundController == null || musicController == null)
            return;

        PlayerPrefs.SetFloat("sound", soundController.audiosource.volume);
        PlayerPrefs.SetFloat("music", musicController.audiosource.volume);
    }
    private void OnEnable()
    {
        ChangeSliderSound();
        txtLevel.text = (PLayerInfo.CurrentLevel - 1).ToString();
    }
    private void ChangeSliderSound()
    {
        if (soundController == null || musicController == null)
            return;

        sliderSound.value = soundController.audiosource.volume;
        sliderMusic.value = musicController.audiosource.volume;
    }
}
