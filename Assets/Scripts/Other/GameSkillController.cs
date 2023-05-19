using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum SkillName { none, boom, star, stop_time, extra_point, fairy };
public class GameSkillController : MonoBehaviour
{
    public List<(int, float)> BonusPoint;

    private Dictionary<SkillName, ISkill> convertStringToSkill = new Dictionary<SkillName, ISkill>() {
        {SkillName.boom,new SkillBoom()},
        {SkillName.star,new SkillStar()},
        {SkillName.stop_time,new SkillStopTime()},
        {SkillName.extra_point,new SkillExtraPonit()},
        {SkillName.fairy,new SkillFairy()}

    };
    private bool currentSkillClick = false;
    private SkillName currentSkillName;

    // Start is called before the first frame update
    private void Awake()
    {
        ServiceLocator.Set(this);
        BonusPoint = new List<(int, float)>();
    }
    private void OnDestroy()
    {
        ServiceLocator.Unset<GameSkillController>();
    }
    public bool IsSkillClick()
    {
        return currentSkillClick;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentSkillName = SkillName.extra_point;
            convertStringToSkill[currentSkillName].DoSkill();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentSkillClick = true;
            currentSkillName = SkillName.fairy;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentSkillClick = true;
            currentSkillName = SkillName.star;
        }
    }
    public bool IsClickSkill()
    {
        return !currentSkillClick;
    }
    public void ClickSkill(SkillName skillName)
    {
        switch (skillName)
        {
            case SkillName.stop_time:
                currentSkillName = SkillName.stop_time;
                convertStringToSkill[currentSkillName].DoSkill();
                break;
            case SkillName.boom:
                currentSkillClick = true;
                currentSkillName = SkillName.boom;
                break;
            case SkillName.extra_point:
                currentSkillName = SkillName.extra_point;
                convertStringToSkill[currentSkillName].DoSkill();
                break;
            case SkillName.fairy:
                currentSkillClick = true;
                currentSkillName = SkillName.fairy;
                break;
            case SkillName.star:
                currentSkillClick = true;
                currentSkillName = SkillName.star;
                break;
        }
    }
    public void DoSkill(params object[] obj)
    {
        convertStringToSkill[currentSkillName].DoSkill(obj);
        currentSkillClick = false;
    }

    public void BonusPotionDown()
    {
        for (int i = 0; i < BonusPoint.Count; i++)
        {
            BonusPoint[i] = (BonusPoint[i].Item1 - 1, BonusPoint[i].Item2);
        }
    }
}
