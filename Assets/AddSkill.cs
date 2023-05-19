using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddSkill : MonoBehaviour
{
    [SerializeField] private Image imgSkill;
    [SerializeField] private TextMeshProUGUI txtAmount;
    [SerializeField] private Button btnClick;
    // Start is called before the first frame update
    public void Init(Sprite img, int amount, SkillName skillName)
    {
        imgSkill.sprite = img;
        txtAmount.text = amount + "";
        btnClick.onClick.RemoveAllListeners();
        btnClick.onClick.AddListener(() =>
        {
            if (!ServiceLocator.Get<GameSkillController>().IsClickSkill()) return;
            ServiceLocator.Get<GameSkillController>().ClickSkill(skillName);
            ServiceLocator.Get<PlayerGameMasterData>().AddData(skillName, -1);
            txtAmount.text = ServiceLocator.Get<PlayerGameMasterData>().GetSkill(skillName) + "";
            if (ServiceLocator.Get<PlayerGameMasterData>().GetSkill(skillName) <= 0) gameObject.SetActive(false);
        });
    }
}
