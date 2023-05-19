using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoom : ISkill
{
    public void DoSkill(params object[] obj)
    {
        ((JewelObj)obj[0]).PowerProcess(52);
        ((JewelObj)obj[0]).Destroy();
    }
}
