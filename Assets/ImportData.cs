using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ImportData : MonoBehaviour
{
    [SerializeField] private Sprite[] imgSkill;
    private void OnEnable()
    {
        Dictionary<SkillName, int> Skills = ServiceLocator.Get<PlayerGameMasterData>().Skills;
        for (int i = 0; i < Skills.Count; i++)
        {
            if (Skills.ElementAt(i).Value <= 0)
            {
                transform.GetChild(i).gameObject.SetActive(false); continue;
            }
            transform.GetChild(i).gameObject.GetComponent<AddSkill>()
                .Init(imgSkill[i], Skills.ElementAt(i).Value, Skills.ElementAt(i).Key);
        }
    }
}
