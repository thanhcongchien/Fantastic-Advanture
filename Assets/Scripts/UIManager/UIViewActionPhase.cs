using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIViewActionPhase : BaseUIView
{
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] Slider sliderTime;
    public Slider SlideTime => sliderTime;
    [SerializeField] TextMeshProUGUI txtScore;
    public TextMeshProUGUI TxtScore => txtScore;
    [SerializeField] Button btnPause;
    public override void Open(params object[] obj)
    {
        base.Open(obj);
        txtLevel.text = $"Level \n {PLayerInfo.CurrentLevel}";
        btnPause.onClick.AddListener(() => { ServiceLocator.GetUIViewManager.ShowUIView<UIViewPause>(); });
    }
    public override void Close()
    {
        base.Close();
    }
}
