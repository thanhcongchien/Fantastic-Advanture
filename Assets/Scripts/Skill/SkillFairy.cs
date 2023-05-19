using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFairy : ISkill
{
    public void DoSkill(params object[] obj)
    {
        ServiceLocator.Get<GameController>().PDestroyType(((JewelObj)obj[0]).GetComponent<JewelObj>().jewel.JewelType, ((JewelObj)obj[0]).transform.position);
    }
}
