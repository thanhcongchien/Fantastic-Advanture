using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIViewSelectLevel : BaseUIView
{

    [SerializeField] Button btnSetting;
    [SerializeField] Button btnOpenSpin;
    [SerializeField] TextMeshProUGUI txtCoint;
    // Start is called before the first frame update
    void Start()
    {
        FisrtAddListener();
    }
    private void OnEnable()
    {
        btnOpenSpin.onClick.AddListener(() => { Close(); ServiceLocator.GetUIViewManager.ShowUIView<UIViewLuckySpin>(); });
        txtCoint.text = ServiceLocator.Get<PlayerGameMasterData>().GetData(DataName.Coint).ToString();
    }
    void FisrtAddListener()
    {
        btnSetting.onClick.AddListener(() => { ServiceLocator.GetUIViewManager.ShowUIView<UIViewSetting>(); });
    }
    // Update is called once per frame
    void Update()
    {

    }

}
