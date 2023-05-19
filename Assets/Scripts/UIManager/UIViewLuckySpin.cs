using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewLuckySpin : BaseUIView
{
    [SerializeField] Button btnClose;

    private void Start()
    {
        btnClose.onClick.AddListener(() => { Close(); ServiceLocator.GetUIViewManager.ShowUIView<UIViewSelectLevel>(); });
    }
}
