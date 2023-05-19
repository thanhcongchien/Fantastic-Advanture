using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SplashSceneManager : BaseSceneManager
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void Start()
    {
        base.Start();

        ServiceLocator.GetUIViewManager.ShowUIView<UIViewSplash>();
        DOVirtual.DelayedCall(2, () => 
        {
            AddressableManager.LoadSingleScene(Definition.SceneDefinition.ADDRESSABLE_LOADING_SCENE,()=> 
            {
                ServiceLocator.GetUIViewManager.CloseUIView<UIViewSplash>();
            });
        });
    }

    protected override void Update()
    {
        base.Update();
    }
}
