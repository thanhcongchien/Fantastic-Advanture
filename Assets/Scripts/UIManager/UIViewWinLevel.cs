using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIViewWinLevel : BaseUIView
{
    [SerializeField] Button btnNext;
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] Image[] starts;
    [SerializeField] Sprite[] spriteStarts;


    [SerializeField] TextMeshProUGUI txtCointReward;
    [SerializeField] GameObject panelReward;
    [SerializeField] GameObject spinCoint;
    [SerializeField] GameObject spinReward;
    // Start is called before the first frame update
    public override void Open(params object[] obj)
    {
        base.Open(obj);
        txtLevel.text = $"Level {PLayerInfo.Info.textlv.text}";
        btnNext.onClick.AddListener(() =>
        {
            ServiceLocator.Get<Timer>().Next();
            ServiceLocator.GetUIViewManager.CloseAllUIView();
        });

        int playerScore = getGameScore(PLayerInfo.Info.Score, Timer.timer.GameTime);
        int star = getGameStar(playerScore);
        for (int i = 0; i < starts.Length; i++)
        {
            if (star - 1 >= i)
                starts[i].sprite = spriteStarts[0];
            else
                starts[i].sprite = spriteStarts[1];
        }
        SaveData();

        int cointGet = getGameCoint(playerScore);
        if (cointGet != 0)
        {
            spinCoint.SetActive(true);
            txtCointReward.text = $"x{cointGet}";
            ServiceLocator.Get<PlayerGameMasterData>().AddData(DataName.Coint, cointGet);
        }
        bool getSpin = ServiceLocator.Get<PlayerGameMasterData>().CheckTotalMapRewardSpin();
        spinReward.SetActive(getSpin);
        panelReward.SetActive(cointGet != 0 || getSpin ? true : false);
    }
    int getGameScore(int playerscore, float gametime)
    {
        return playerscore + (int)Mathf.Abs(gametime) * 500;
    }
    int getGameStar(int score)
    {
        if (score >= 80000)
        {
            PLayerInfo.MapPlayer.Stars = 3;
            return 3;
        }
        else if (score >= 60000)
        {
            if (PLayerInfo.MapPlayer.Stars < 2)
                PLayerInfo.MapPlayer.Stars = 2;
            return 2;
        }
        else
        {
            PLayerInfo.MapPlayer.Stars = 1;
            return 1;
        }
    }
    int getGameCoint(int score)
    {
        if (score <= 0)
        {
            return 0;
        }
        else if (score <= 5000)
        {
            return 0;
        }
        else if (score <= 7000)
        {
            return 5;
        }
        else if (score <= 15000)
        {
            return 7;
        }
        else if (score <= 20000)
        {
            return 20;
        }
        else if (score <= 40000)
        {
            return 30;
        }
        else if (score <= 60000)
        {
            return 100;
        }
        else if (score <= 80000)
        {
            return 150;
        }
        else if (score >= 80000)
        {
            return 250;
        }
        else
        {
            return 0;
        }
    }
    void SaveData()
    {
        int index = PLayerInfo.MapPlayer.Level - 1;
        DataLoader.MyData[index] = PLayerInfo.MapPlayer;
        if (PLayerInfo.MapPlayer.Level < 297)
            DataLoader.MyData[index + 1].Locked = false;
        PlayerUtils p = new PlayerUtils();
        p.Save(DataLoader.MyData);
    }
}
