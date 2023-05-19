using EasyUI.PickerWheelUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LuckySpinController : MonoBehaviour
{
    [SerializeField] private Button uiSpinButton;
    [SerializeField] private PickerWheel uiPickerWheel;
    [SerializeField] private TextMeshProUGUI txtCount;
    // Start is called before the first frame update
    void Start()
    {
        uiSpinButton.onClick.AddListener(() =>
        {
            if (ServiceLocator.Get<PlayerGameMasterData>().GetData(DataName.CountSpint) <= 0) return;
            uiSpinButton.interactable = false;
            uiPickerWheel.OnSpinEnd(uiWheel =>
            {
                ServiceLocator.Get<PlayerGameMasterData>().AddData(DataName.CountSpint, -1);
                txtCount.text = "x" + ServiceLocator.Get<PlayerGameMasterData>().GetData(DataName.CountSpint);
                uiSpinButton.interactable = true;
                ServiceLocator.GetUIViewManager.ShowUIView<UIViewNotification>("Notification", $"You get {uiWheel.Label}");
                if (uiWheel.Amount == 1)
                    ServiceLocator.Get<PlayerGameMasterData>().AddData((SkillName)System.Enum.Parse(typeof(SkillName), uiWheel.KEY), uiWheel.Amount);
                else
                    ServiceLocator.Get<PlayerGameMasterData>().AddData((DataName)System.Enum.Parse(typeof(DataName), uiWheel.KEY), uiWheel.Amount);
                Debug.LogError($"Spin end label: {uiWheel.Label}|{uiWheel.Amount}");
            });
            uiPickerWheel.Spin();
        });
    }
    private void OnEnable()
    {
        txtCount.text = "x" + ServiceLocator.Get<PlayerGameMasterData>().GetData(DataName.CountSpint);
    }
}
