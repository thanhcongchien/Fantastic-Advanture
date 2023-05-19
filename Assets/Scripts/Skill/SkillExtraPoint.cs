using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExtraPonit : ISkill
{ 
    private int count = 5;
    private int bonus = 2;
    public void DoSkill(params object[] obj)
    {
        ServiceLocator.Get<GameSkillController>().BonusPoint.Add((count,bonus));
    }
}
