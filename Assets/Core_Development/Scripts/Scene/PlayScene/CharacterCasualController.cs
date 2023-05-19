using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DG.Tweening;

public class CharacterCasualController : MonoBehaviour
{
    [SerializeField] SkeletonGraphic skeletonGraphic;

    public void PlayAnimation(string keyAnimation,bool isLoop = false)
    {
        if (string.IsNullOrEmpty(keyAnimation) || string.IsNullOrWhiteSpace(keyAnimation))
            return;

        if (skeletonGraphic == null)
            return;

        skeletonGraphic.AnimationState.SetAnimation(0, keyAnimation, isLoop);
    }
}
