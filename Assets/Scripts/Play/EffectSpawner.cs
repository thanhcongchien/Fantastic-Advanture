﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class EffectSpawner : MonoBehaviour
{
    public JewelSpawner jewelSpawner;
    public static EffectSpawner effect;

    public GameObject parent;

    public GameObject[] EffectPrefabs;
    public GameObject[] MovingEffect;

    public Animator redglass;

    public GameObject[,] JewelCrashArray;
    public GameObject JewelCrashParent;

    public UnityEngine.UI.Text level;
    public UnityEngine.UI.Text best;
    public TextMeshProUGUI Score;
    public UnityEngine.UI.Image Energy;

    public float REFRESH_COMBO_TIME = 2f;

    private const float BOOM_TIME = 0.5f;

    private const float ICECRASH_TIME = 0.5f;

    private const float JEWELCASH_TIME = 0.35f;

    private const float SCORESHOW_TIME = 0.5f;

    private const float THUNDER_TIME = 0.4f;

    private const float FILEARROW_TIME = 0.4f;

    private int ComboCount = 0;

    private int ThunderCount = 0;

    private int PowerCount = 0;

    public float ComboCountdown;

    float EnergyStack = 0;

    bool isEnergyInc;
    private float LightingWidth = 0f;
    private float itemWidth = 90f;

    void Awake()
    {
        effect = this;
        JewelCrashArray = new GameObject[7, 9];
    }
    private void Start()
    {
        Score = ((UIViewActionPhase)ServiceLocator.GetUIViewManager.GetBaseUiView<UIViewActionPhase>()).TxtScore;
    }

    public void ContinueCombo()
    {
        ComboCountdown = REFRESH_COMBO_TIME;
    }

    public void ComBoInc()
    {
        ComboCount++;
    }

    public void ScoreInc(Vector3 pos, Jewel jewel)
    {
        int scorebonus = 10 + ComboCount * 10;
        if (PLayerInfo.MODE != 1)
        {
            if (PLayerInfo.Info.Score < PLayerInfo.MapPlayer.Level * 5000)
                Timer.timer.ScoreBarProcess(scorebonus);
            else if (GameController.action.GameState == (int)Timer.GameState.PLAYING)
            {
                Timer.timer.ClassicLvUp();
            }
        }
        else
        {
            float bonusMutiple = 1;
            List<(int, float)> bonusPoint = ServiceLocator.Get<GameSkillController>().BonusPoint;
            if (bonusPoint.Count > 0)
            {

                for (int i = 0; i < bonusPoint.Count; i++)
                {
                    bonusMutiple += float.Parse(bonusPoint[i].Item2.ToString());
                    if (int.Parse(bonusPoint[i].Item1.ToString()) <= 0)
                    {
                        bonusPoint.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
            }
            scorebonus *= Mathf.FloorToInt(bonusMutiple);
            if (GameController.action.GameState == (int)Timer.GameState.PLAYING)
                PLayerInfo.Info.Score += scorebonus;
            BonusEffect();
            MiniStar(pos, jewel);
        }

        ScoreEff(scorebonus, pos);
        SetScore(PLayerInfo.Info.Score);
    }

    private void BonusEffect()
    {
        ThunderCount++;
        PowerCount++;
        EnergyStack += 1 / 21f;
        EnergyInc();
        if (ThunderCount >= 21)
        {
            GameController.action.DestroyRandom();
            ThunderCount = 0;
            Energy.fillAmount = 0;
            EnergyStack = 0;
        }
        if (PowerCount >= 32)
        {
            PowerCount = 0;
            GameController.action.isAddPower = true;
        }
    }

    private void EnergyInc()
    {
        if (!isEnergyInc)
            StartCoroutine(IEEnergyInc());
    }

    IEnumerator IEEnergyInc()
    {
        // Debug.Log (Energy.gameObject.transform.position);
        isEnergyInc = true;
        float d = 1 / 210f;
        while (EnergyStack > 0)
        {
            Energy.fillAmount += d;
            EnergyStack -= d;
            yield return null;
            if (Energy.fillAmount == 1)
                Energy.fillAmount = 0;
        }
        EnergyStack = 0;
        isEnergyInc = false;
    }

    private void ScoreEff(int score, Vector3 pos)
    {
        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[4]);
        tmp.transform.GetChild(0).GetComponent<TextMesh>().text = score.ToString();
        tmp.transform.SetParent(parent.transform, false);
        tmp.transform.position = new Vector3(pos.x, pos.y, tmp.transform.position.z);
        Destroy(tmp, SCORESHOW_TIME);
    }

    public void SetLevel(int lv)
    {
        level.text = lv.ToString();
    }

    public void SetBest(int bestscore)
    {
        best.text = bestscore.ToString();
    }

    public void SetScore(int _score)
    {
        Score.text = _score.ToString();
    }
    public GameObject JewelCash(Vector3 pos)
    {
        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[0]);
        tmp.transform.SetParent(JewelCrashParent.transform, false);
        tmp.transform.localPosition = new Vector3(pos.x, pos.y, -0.2f);
        return tmp;
        // Destroy(tmp, JEWELCASH_TIME);
    }

    public void Thunder(Vector3 pos)
    {
        // Debug.Break();
        //GameObject tmp = (GameObject)Instantiate(EffectPrefabs[3]);
        //tmp.transform.SetParent(parent.transform, false);
        //tmp.transform.position = new Vector3 (pos.x,pos.y,-2.1f);
        //Destroy(tmp, THUNDER_TIME);

        MGE(Energy.transform.position, pos, -0.4f);
    }

    public void boom(Vector3 pos)
    {
        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[1]);
        SoundController.Sound.Boom();
        tmp.transform.SetParent(parent.transform, false);
        tmp.transform.position = pos;
        Destroy(tmp, BOOM_TIME);
    }

    public void Enchant(GameObject obj)
    {
        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[2]);
        tmp.transform.SetParent(obj.transform, false);
    }

    public void ThunderRow(GameObject obj, int power)
    {
        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[5]);
        tmp.transform.SetParent(obj.transform.GetChild(0).transform, false);
        if (power == 3)
            tmp.transform.localEulerAngles = new Vector3(0, 0, 90);
    }

    public void ReloadImagePower(JewelObj jewelObj, int power)
    {
        if (power == 3 || power == 4)
            jewelObj.render.sprite = ServiceLocator.Get<JewelSpawner>().JewelSprite.GetSprite($"{jewelObj.jewel.JewelType}{jewelObj.jewel.JewelPower}");
    }

    public void FireArrow(Vector3 pos, bool c)
    {
        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[6]);
        tmp.transform.SetParent(parent.transform, false);
        tmp.transform.position = new Vector3(pos.x, pos.y, -2.2f);
        if (c)
            tmp.transform.localEulerAngles = new Vector3(0, 0, 90);
        Destroy(tmp, FILEARROW_TIME);
    }

    public void Clock(GameObject obj)
    {
        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[7]);
        tmp.transform.SetParent(obj.transform.GetChild(0).transform, false);
    }

    public void StarWinEffect(Vector3 pos)
    {
        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[8]);
        tmp.transform.SetParent(parent.transform, false);
        tmp.transform.position = new Vector3(pos.x, pos.y, tmp.transform.position.z);
        Animation anim = tmp.GetComponent<Animation>();
        StarEffectAnim(anim, tmp);
        Destroy(tmp, 1f);

    }

    public void IceCrash(Vector2 pos)
    {

        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[9]);
        tmp.transform.SetParent(parent.transform, false);
        tmp.transform.position = GribManager.cell.GribCell[(int)pos.x, (int)pos.y].transform.position;
        Destroy(tmp, ICECRASH_TIME);

    }
    public void LockCrash(Vector2 pos)
    {

        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[10]);
        tmp.transform.SetParent(parent.transform, false);
        tmp.transform.position = GribManager.cell.GribCell[(int)pos.x, (int)pos.y].transform.position;
        Destroy(tmp, ICECRASH_TIME);

    }

    void StarEffectAnim(Animation anim, GameObject tmp)
    {
        //Debug.Break();
        anim.enabled = true;
        AnimationClip animclip = new AnimationClip();
#if UNITY_5
                animclip.legacy = true;
#endif
        AnimationCurve curveScalex = AnimationCurve.Linear(0, tmp.transform.localScale.x, 1, 3);
        //AnimationCurve curveScaley = AnimationCurve.Linear(0, tmp.transform.localScale.y, 1, 3);
        AnimationCurve curvex = AnimationCurve.Linear(0, tmp.transform.position.x, 1, 0);
        AnimationCurve curvey = AnimationCurve.Linear(0, tmp.transform.position.y, 1, 0);
        AnimationCurve curvez = AnimationCurve.Linear(0, tmp.transform.position.z, 1, tmp.transform.position.z);
        AnimationCurve curveColora = AnimationCurve.Linear(0, 1, 1, 0);

        animclip.SetCurve("", typeof(Transform), "m_LocalScale.x", curveScalex);
        animclip.SetCurve("", typeof(Transform), "m_LocalScale.y", curveScalex);
        animclip.SetCurve("", typeof(Transform), "localPosition.x", curvex);
        animclip.SetCurve("", typeof(Transform), "localPosition.y", curvey);
        animclip.SetCurve("", typeof(Transform), "localPosition.z", curvez);
        animclip.SetCurve(tmp.transform.GetChild(0).name, typeof(SpriteRenderer), "m_Color.a", curveColora);
        // animclip.SetCurve("", typeof(Animation), "m_Enabled", curvenable);
        anim.wrapMode = WrapMode.Once;
        anim.AddClip(animclip, "Startwin");
        anim.Play("Startwin");
    }

    public IEnumerator ComboTick()
    {
        while (true)
        {
            if (ComboCountdown > 0)
                ComboCountdown -= Time.deltaTime;
            else
                ComboCount = 0;
            yield return null;
        }
    }

    public GameObject MGE(Vector3 pos, Vector3 target, bool isSingle = true)
    {
        Debug.LogError("Stop");
        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[11]);
        tmp.transform.SetParent(parent.transform, false);
        if (isSingle)
        {
            tmp.transform.position = new Vector3(-1.68f, 3.07f, -0.22f);
        }
        else
        {
            tmp.transform.position = new Vector3(pos.x, pos.y, -0.22f);
        }
        float AngleRad = Mathf.Atan2(target.y - pos.y, target.x - pos.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        tmp.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        //var distance = Mathf.Sqrt(Mathf.Pow(pos.x - target.x,2) + Mathf.Pow(pos.y - target.y, 2));
        //var tempIndex = LightingWidth / itemWidth;
        //tmp.transform.localScale = new Vector3(distance / tempIndex, tmp.transform.localScale.y, tmp.transform.localScale.z);
        //Ulti.MoveTo(tmp, target, 0.4f);
        LeanTween.move(tmp, new Vector2(target.x+0.4f, target.y), 0.4f).setOnComplete(
            s =>
            {
                LeanTween.delayedCall(0.4f, () =>
                {
                    Destroy(tmp);
                });
            });
        SoundController.Sound.Gun();

        return tmp;
    }

    public void Blur(Vector3 pos)
    {
        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[13]);
        tmp.transform.SetParent(parent.transform, false);
        tmp.transform.position = pos;
        Destroy(tmp, 0.4f);

    }

    public GameObject MGE(Vector3 pos, Vector3 target, float z)
    {

        GameObject tmp = MGE(pos, target);
        //tmp.transform.position += new Vector3(pos.x, pos.y, z);
        return tmp;
    }

    public void glass()
    {
        if (PLayerInfo.MODE == 1)
        {
            redglass.SetTrigger("Active");
        }//redglass.Play("glass");
        //Debug.Log("bla");
    }

    public void MiniStar(Vector3 startpos, Jewel jewel)
    {
        return;
        GameObject tmp = (GameObject)Instantiate(EffectPrefabs[12]);
        if (jewel.JewelType != 8)
        {
            tmp.GetComponent<SpriteRenderer>().sprite = jewelSpawner.JewelSprite.GetSprite(jewel.JewelType + "");
        }
        else
        {
            tmp.GetComponent<SpriteRenderer>().sprite = jewelSpawner.JewelSprite.GetSprite("7");
        }
        tmp.transform.SetParent(parent.transform, false);
        tmp.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        Ulti.MoveTo(tmp, startpos, new Vector2(-2.485f, 4.418f), 1.2f, -2.2f);
        Destroy(tmp, 1.2f);
    }
}
