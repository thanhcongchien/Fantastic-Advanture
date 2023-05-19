using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIViewNotification : BaseUIView
{
    [SerializeField] TextMeshProUGUI txtTitle;
    [SerializeField] TextMeshProUGUI txtNotification;
    [SerializeField] Button btnClose;
    [SerializeField] Button btnOK;
    public override void Open(params object[] obj)
    {
        base.Open();
        txtTitle.text = (string)obj[0];
        txtNotification.text = (string)obj[1];
        btnClose.onClick.AddListener(() => { Close(); });
        btnOK.onClick.AddListener(() => { Close(); });

    }
}
