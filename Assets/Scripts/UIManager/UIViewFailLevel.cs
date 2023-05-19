using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewFailLevel : BaseUIView
{
    [SerializeField] Button btnReplay;
    [SerializeField] Button btnGoHome;
    public override void Open(params object[] obj)
    {
        base.Open(obj);

        btnReplay.onClick.AddListener(() => { ServiceLocator.Get<Timer>().Restart(); });
        btnGoHome.onClick.AddListener(() => { ServiceLocator.Get<Timer>().Home(); Close(); });
    }
}
