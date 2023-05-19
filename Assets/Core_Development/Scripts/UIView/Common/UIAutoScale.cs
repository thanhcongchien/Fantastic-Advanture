using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

[RequireComponent(typeof(RectTransform))]
public class UIAutoScale : MonoBehaviour
{
    [SerializeField] bool isLoop;
    [SerializeField] float durationScale;
    [SerializeField] Ease easeScale;
    [SerializeField] Vector3 startScale;
    [SerializeField] Vector3 endScale;
    Tween twScale;
    Guid guidTwScale;

    RectTransform rtrfOwner;

    private void Awake()
    {
        rtrfOwner = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        DoScale();
    }

    private void OnDisable()
    {
        KillScale();
    }

    private void KillScale() 
    {
        if (twScale != null)
        {
            DOTween.Kill(guidTwScale);
        }
    }

    private void DoScale() 
    {
        if (rtrfOwner == null)
            return;

        KillScale();

        guidTwScale = Guid.NewGuid();
        rtrfOwner.localScale = startScale;

        if (isLoop)
        {
            twScale = rtrfOwner.DOScale(endScale, durationScale).SetLoops(-1, LoopType.Yoyo).SetEase(easeScale);
        }
        else
        {
            twScale = rtrfOwner.DOScale(endScale, durationScale).SetEase(easeScale);
        }

        twScale.id = guidTwScale;
        twScale.SetUpdate(true);
    }
}
