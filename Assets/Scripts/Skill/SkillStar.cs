using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStar : ISkill
{
    public void DoSkill(params object[] obj)
    {
        ((JewelObj)obj[0]).PowerProcess(2);
        ((JewelObj)obj[0]).PowerProcess(3);
        ((JewelObj)obj[0]).Destroy();
    }
}