using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGameMasterData : MonoBehaviour
{
    public int CountMapRewardSpin = 4;
    public DataName[] ListDataNames;
    public SkillName[] ListSkillNames;
    private Dictionary<DataName, int> Datas;
    public Dictionary<SkillName, int> Skills;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Datas = new Dictionary<DataName, int>();
        Skills = new Dictionary<SkillName, int>();
        for (int i = 0; i < ListDataNames.Length; i++)
        {
            Datas.Add(ListDataNames[i], 0);
        }
        for (int i = 0; i < ListSkillNames.Length; i++)
        {
            Skills.Add(ListSkillNames[i], 0);
        }
        ServiceLocator.Set(this);
        LoadAllData();
    }
    private void OnDestroy()
    {
        SaveAllData();
        //ServiceLocator.Unset<PlayerGameMasterData>();
    }
    public void AddData(string name, int value)
    {
        try
        {
            SkillName key = (SkillName)System.Enum.Parse(typeof(SkillName), name);
            AddData(key, value);
        }
        catch
        {
            DataName data = (DataName)System.Enum.Parse(typeof(DataName), name);
            AddData(data, value);
        }
    }
    public void AddData(DataName dataName, int value)
    {
        Datas[dataName] += value;
    }
    public void AddData(SkillName skillName, int value)
    {
        Skills[skillName] += value;
    }
    public void ChangeData(DataName dataName, int value)
    {
        Datas[dataName] = value;
    }
    public void ChangeSkill(SkillName skillName, int value)
    {
        Skills[skillName] = value;
    }
    public int GetData(DataName dataName)
    {
        return Datas[dataName];
    }
    public int GetSkill(SkillName skillName)
    {
        return Skills[skillName];
    }

    private void SaveAllData()
    {
        for (int i = 0; i < Skills.Count; i++)
        {
            PlayerPrefs.SetInt(Skills.ElementAt(i).Key.ToString(), Skills.ElementAt(i).Value);
        }

        for (int i = 0; i < Datas.Count; i++)
        {
            PlayerPrefs.SetInt(Datas.ElementAt(i).Key.ToString(), Datas.ElementAt(i).Value);
        }
    }
    private void LoadAllData()
    {
        for (int i = 0; i < Skills.Count; i++)
        {
            Skills[Skills.ElementAt(i).Key] = PlayerPrefs.GetInt(Skills.ElementAt(i).Key.ToString());
        }

        for (int i = 0; i < Datas.Count; i++)
        {
            Datas[Datas.ElementAt(i).Key] = PlayerPrefs.GetInt(Datas.ElementAt(i).Key.ToString());
        }
    }

    public bool CheckTotalMapRewardSpin()
    {
        AddData(DataName.TotalMapRewardSpin, 1);
        if (GetData(DataName.TotalMapRewardSpin) >= CountMapRewardSpin)
        {
            ChangeData(DataName.TotalMapRewardSpin, 0);
            AddData(DataName.CountSpint, 1);
            return true;
        }
        return false;
    }
}
public enum DataName
{                                                                       
    Coint,
    TotalStar,
    TotalMapRewardSpin,
    CountSpint
}
