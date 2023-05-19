using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIViewReadyPlay : BaseUIView
{
    [SerializeField] Button btnClose;
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] Image[] starts;
    [SerializeField] Button btnPlay;
    [SerializeField] Sprite[] spriteStarts;

    private void Awake()
    {
        btnClose.onClick.AddListener(() => {
            Close();
            ServiceLocator.Get<CameraMovement>().UnfreezeMap();
        });
        btnPlay.onClick.AddListener(() =>
        {
            ServiceLocator.Get<CameraMovement>().ArcadeScene();
            Close();
        });
    }

    public override void Open(params object[] obj)
    {
        base.Open();
        Player player = (Player)obj[0];
        txtLevel.text = $"Level {player.Level}";

        

        for (int i = 0; i < starts.Length; i++)
        {
            if (player.Stars - 1 >= i)
                starts[i].sprite = spriteStarts[0];
            else
                starts[i].sprite = spriteStarts[1];
        }

    }
}
