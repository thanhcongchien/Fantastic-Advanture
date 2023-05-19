using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SkillStopTime : ISkill
{
    private int timeStopTime = 60;
    public void DoSkill(params object[] obj)
    {
        SkillAction();
    }
    private async void SkillAction()
    {
        ServiceLocator.Get<Timer>().AddGameTime = timeStopTime;

    }
}